require('dotenv').config()
const express = require('express');
const GameState = require('./lib/GameState');
const GameWallet = require('./lib/GameWallet');
const TransitWallet = require('./lib/TransitWallet');
const PoolWallet = require("./lib/PoolWallet");
const app = express();
const cors = require('cors');
app.use(cors());
const { getId, getRandomNumber, number2Hash } = require('./helpers.js')

const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000

const gameState = new GameState()
const poolWallet = new PoolWallet();

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

app.get('/', (req, res) => {
    res.send('This is RiskPools backend. Docs: /api-docs')
})

app.get('/upload-game-wallet', async (req, res) => {
    console.log('Uploading wallet: ' + req.query.address)
    let address = req.query.address;
    let privateKey = req.query.privateKey;
    let publicKey = req.query.publicKey;
    await gameState.saveWallet(address, privateKey, publicKey);
    console.log("Wallet " + address + " was saved in DB!");
    return res.status(200).json({ success: true })
})

app.get('/play-game', async (req, res) => {
    console.log('Play game with wallet: ' + req.query.gameWalletAddress)
    let round = req.query.round;
    let bid = Number(req.query.bid);
    if (bid < Number(process.env.MIN_BID) || bid > Number(process.env.MAX_BID)) {
        res.status(400).json({ error: "Incorrect bid amount" })
    }
    let gameWalletAddress = req.query.gameWalletAddress;
    let gameWalletPrivateKey = req.query.gameWalletPrivateKey;
    let gameWalletPublicKey = req.query.gameWalletPublicKey;
    const gameWalletKeyPair = {
        address: gameWalletAddress,
        privateKey: gameWalletPrivateKey,
        publicKey: gameWalletPublicKey
    }
    console.log(gameWalletKeyPair)
    let gameId = getId();
    console.log('Game ID: ' + gameId);
    startGame(round, bid, gameWalletKeyPair, gameId) // this is async, and we don't wait for finish execution
    return res.status(200).json({ gameid: gameId })
})

app.get('/game-status', async (req, res) => {
    console.log('Checking game status: ' + req.query.gameid)
    let gameId = req.query.gameid;
    let gameCheck = await gameState.getGameStatus(gameId)
    return res.status(200).json(gameCheck)
})

async function startGame(round, bid, gameWalletKeyPair, gameId) {
    console.log('Game was put in DB ' + gameId)
    await gameState.onStartGame(gameId); // pending
    // Open game wallet for this player
    const gameWallet = new GameWallet(gameWalletKeyPair.privateKey);
    await gameWallet.asyncLoaded()
    const gameWalletBalance = await gameWallet.getBalance()
    console.log('Game wallet balance: ' + gameWalletBalance)

    // Generate transit wallet for this player
    const transitWallet = new TransitWallet();
    await transitWallet.asyncLoaded()
    //Estimate sum to send from GameWallet to Transit wallet. If the Game Wallet balance is sufficient for withdrawing
    // both bid and commission - we withdraw both. It Game Wallet Balance > bid. but < bid + comission - we withdraw
    // (bid - commission) + comission = bid
    let sumToSend = await gameWallet.sendToTransit(bid, transitWallet.address)
    console.log('Funds sent from game wallet to transit wallet: UBX ' + sumToSend)
    console.log('Transit wallet balance: ' + await transitWallet.getBalance());
    console.log(transitWallet.keypair);


    let currentBalance = bid || 0;

    console.log('Number of rounds: ' + round);
    const tParityList = [];
    for (let i = 0; i < round; i++) {
        console.log('Playing round: ' + i);
        const randomNumber = getRandomNumber();
        const hash = number2Hash(randomNumber);
        const parity = randomNumber % 2 !== 0;

        if (currentBalance <= 0) {
            break;
        }

        if (parity) {
            currentBalance = Number(currentBalance) + Number(bid);
        } else {
            if (Number(currentBalance) - Number(bid) < 0) { return; }
            else {
                currentBalance = Number(currentBalance) - Number(bid);
            }
        }

        tParityList.push({
            round: i,
            number: randomNumber,
            parity: parity,
            currentBalance: currentBalance,
            hashNumber: hash,
        });
        const incrementBalance = currentBalance / round;  // Split the current balance into parts based on rounds
        if (incrementBalance > 0) {
            await poolWallet.sendToGameAndProject(incrementBalance, gameWallet.address);
            await gameState.saveOnProcess(gameId, tParityList)
            console.log(`Incremental balance sent: ${incrementBalance}`);
        }
    }
    if (currentBalance >= (bid || 0)) {
        await transitWallet.sendToPool()
    } else {
        // Send from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
        console.log('Sending all funds were sent from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet')
        console.log('Transit wallet balance: ' + await transitWallet.getBalance())
        await transitWallet.sendToPoolProjectAndProfit()
        console.log('From transit wallet funds were sent: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet')
    }
    await gameState.saveOnFinish(gameId, tParityList, gameWalletKeyPair, transitWallet.keypair)
    return { success: true, parityList: tParityList }
};

app.listen(port, async () => {
    await poolWallet.asyncLoaded()
    await gameState.onStartServer()
    console.log(`RiskPool backend listening on port ${port}`);
    setInterval(gameState.performRefunds, 1000 * 60 * 60 * 24);
})
