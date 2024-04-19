require('dotenv').config()
const { Level } = require('level');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const cors = require('cors');
const app = express();
const corsOptions = {
  origin: process.env.CORS_ORIGIN,
  optionsSuccessStatus: 200,
};
const { getRandomNumber, number2Hash, hash2Number, initCilUtils } = require('./helpers.js')

app.use(cors(corsOptions));
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000
const t = process.env.REFUNDS_DB_PATH
const db = new Level(process.env.REFUNDS_DB_PATH, { valueEncoding: 'json' })
const db_operations_log = new Level(process.env.OPERATIONS_LOG_DB_PATH, { valueEncoding: 'json' })

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

app.get('/', (req, res) => {
  res.send('This is RiskPools backend. Docs: /api-docs')
})

app.get('/upload-game-wallet', async (req, res) => {
  console.log('Uploading wallet: ' + req.query.address)
  let address = req.query.address;
  let privateKey = req.query.privateKey;
  let publicKey = req.query.publicKey;
  let gameWallet = {
    address: address,
    privateKey: privateKey,
    publicKey: publicKey,
    isRefunded: false,
  };
  await db.put("gameWallet_" + address, gameWallet);
  console.log("Wallet " + address + " was saved in DB!");
  return res.status(200).json({ success: true })
})

app.get('/play-game', async (req, res) => {
  console.log('Play game with wallet: ' + req.query.gameWalletAddress)
  let round = req.query.round;
  let bid = Number(req.query.bid);
  let gameWalletAddress = req.query.gameWalletAddress;
  let gameWalletPrivateKey = req.query.gameWalletPrivateKey;
  let gameWalletPublicKey = req.query.gameWalletPublicKey;
  gameWalletKeyPair = {
    address: gameWalletAddress,
    privateKey: gameWalletPrivateKey,
    publicKey: gameWalletPublicKey
  }
  console.log(gameWalletKeyPair)
  let jsonres = await startGame(round, bid, gameWalletKeyPair)
  return res.status(200).json(jsonres)
})

async function performRefunds() {
  for await (const key of db.keys()) {
    let gameWallet = await db.get(key);
    if (!gameWallet.isRefunded) {
      await performRefund(gameWallet);
      db.del(key);
    }
  }
}


async function performRefund(gameWallet) {
  console.log('Performing refunds for wallet: ' + gameWallet.address);
  let gameWalletCilUtils = new CilUtils({
    privateKey: gameWallet.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  await gameWalletCilUtils.asyncLoaded();
  const txList = await gameWalletCilUtils.getTXList();
  for (j = 0; j < txList.length; j++) {
    if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == gameWallet.address) {
      const balance = await gameWalletCilUtils.getBalance()
      console.log('Performing refund');
      console.log('Balance: ' + balance)
      console.log(txList[j]);
      console.log("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from)
      let txFunds = await gameWalletCilUtils.createSendCoinsTx([
        [txList[j].inputs[0].from, -1]], 0);
      await gameWalletCilUtils.sendTx(txFunds);
      await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      console.log('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from)
    }
  }
}

async function startGame(round, bid, gameWalletKeyPair) {
  // Open game wallet for this player
  gameWalletCilUtils = new CilUtils({
    privateKey: gameWalletKeyPair.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  await gameWalletCilUtils.asyncLoaded();
  console.log('Game wallet balance: ' + await gameWalletCilUtils.getBalance())

  // Generate transit wallet for this player
  transitWalletKeyPair = crypto.createKeyPair();
  transitWalletCilUtils = new CilUtils({
    privateKey: transitWalletKeyPair.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  await transitWalletCilUtils.asyncLoaded();
  const txFunds = await gameWalletCilUtils.createSendCoinsTx([
    [transitWalletKeyPair.address, bid]], 0);
  await gameWalletCilUtils.sendTx(txFunds);
  await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
  console.log('Funds sent from game wallet to transit wallet: UBX ' + bid)


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
  }
  console.log(tParityList)
  if (currentBalance >= (bid || 0)) {
    // Send all funds from transit wallet to pool wallet
    let txFunds = await transitWalletCilUtils.createSendCoinsTx([
      [global.poolWalletKeyPair.address, -1]], 0);
    await transitWalletCilUtils.sendTx(txFunds);
    await transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log('All funds were sent from transit wallet to pool wallet')

    // Send CurrentBalance from pool wallet to game wallet
    txFunds = await global.poolWalletCilUtils.createSendCoinsTx([
      [global.gameWalletKeyPair.address, currentBalance]], 0);
    await global.poolWalletCilUtils.sendTx(txFunds);
    await global.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log('CurrentBalance was sent from pool wallet to game wallet')

    // Send 2% of CurrentBalance from pool wallet to project Wallet
    txFunds = await global.poolWalletCilUtils.createSendCoinsTx([
      [global.projectWalletKeyPair.address, currentBalance * 0.02]], 0);
    await global.poolWalletCilUtils.sendTx(txFunds);
    await global.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log('2% of CurrentBalance was sent from pool wallet to project Wallet')
  } else {
    //Estimate funds available for sending
    const arrUtxos = await transitWalletCilUtils.getUtxos();
    const walletBalance = arrUtxos.reduce((accum, current) => accum + current.amount, 0);
    const txCost = transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
    const sumToSend = walletBalance - txCost;

    // Send from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
    const txFunds = await transitWalletCilUtils.createSendCoinsTx([
      [global.projectWalletKeyPair.address, sumToSend * 0.02],
      [global.profitWalletKeyPair.address, sumToSend * 0.784],
      [global.poolWalletKeyPair.address, sumToSend * 0.196]
    ], 0);
    await transitWalletCilUtils.sendTx(txFunds);
    await global.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log('From transit wallet funds were sent: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet')
  }
  let numberOfGamesInDb = await db_operations_log.get("numberOfGames");
  numberOfGamesInDb = Number(numberOfGamesInDb) + 1;
  await db_operations_log.put("gameResult_" + numberOfGamesInDb, [gameWalletKeyPair, transitWalletKeyPair, tParityList]);
  await db_operations_log.put("numberOfGames", numberOfGamesInDb);
  return { success: true, parityList: tParityList }
};

app.listen(port, async () => {
  initCilUtils();
  const value = await db_operations_log.get("numberOfGames", (err) => {
    db_operations_log.put("numberOfGames", 1)
  })

  console.log(`RiskPool backend listening on port ${port}`);
  setInterval(performRefunds, 1000 * 60 * 60 * 24);
})