require('dotenv').config()
const { Level } = require('level');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const app = express();
const cors = require('cors');
app.use(cors());
const { getId, getRandomNumber, number2Hash, hash2Number, initCilUtils, randomDelay } = require('./helpers.js')

const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000
const db = new Level(process.env.REFUNDS_DB_PATH, { valueEncoding: 'json' })
const db_operations_log = new Level(process.env.OPERATIONS_LOG_DB_PATH, { valueEncoding: 'json' })
const db_game_statuses = new Level(process.env.GAME_STATUSES_DB_PATH, { valueEncoding: 'json' })
const db_game_wallets = new Level(process.env.GAME_WALLETS_DB_PATH, { valueEncoding: 'json' })
db.has = async function (key) {
  try {
    await this.get(key);
    return true;
  } catch (e) {
    if (e.notFound) {
      return false;
    } else {
      throw e; // rethrow other exceptions
    }
  }
};
db_operations_log.has = db.has
db_game_wallets.has = db.has
db_game_statuses.has = db.has

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
  try {
    let round = req.query.round;
    let bid = Number(req.query.bid);
    if (bid < Number(process.env.MIN_BID) || bid > Number(process.env.MAX_BID)) {
      res.status(400).json({ error: "Incorrect bid amount" })
    }
    let uid = req.query.uid
    gameWallet = await db_game_wallets.get(uid)
    gameWalletKeyPair = {
      address: gameWallet.address,
      privateKey: gameWallet.privateKey,
      publicKey: gameWallet.publicKey,
    }
    let gameId = getId();
    console.log('Game ID: ' + gameId);
    startGame(round, bid, gameWalletKeyPair, gameId) // this is async, and we don't wait for finish execution
    return res.status(200).json({ gameid: gameId })
  } catch (e) {
    console.error(e)
    return res.status(501).json({ error: "The game ended unexpectedly" })
  }
})

app.get('/game-status', async (req, res) => {
  console.log('Checking game status: ' + req.query.gameid)
  let gameId = req.query.gameid;
  let gameCheck = null
  let value = await db_game_statuses.get(gameId)
  if (value) {
    if (value[0]) {
      gameCheck = { status: value[0], caption: "Game finished", parityList: value[1] }
    } else {
      gameCheck = { status: value[0], caption: "Game in progress", parityList: value[1] }
    }
  } else {
    gameCheck = { status: -1, caption: "Game doesn't exist", parityList: null }
  }
  return res.status(200).json(gameCheck)
})

app.get('/create-game-wallet', async (req, res) => {
  let uid = req.query.uid
  console.log('Creating game wallet for user: ' + uid)
  let gameWallet = {}
  isRestored = true
  if (await db_game_wallets.has(uid)) {
    gameWallet = await db_game_wallets.get(uid)
  } else {
    console.log('Wallet for uid not found')
    gameWalletKeyPair = crypto.createKeyPair();
    gameWallet = {
      address: gameWalletKeyPair.address,
      privateKey: gameWalletKeyPair.privateKey,
      publicKey: gameWalletKeyPair.publicKey,
    }
    db_game_wallets.put(uid, gameWallet)
    isRestored = false
  }
  console.log("Address: " + gameWallet.address)
  return res.status(200).json({ "isRestored": isRestored, "address": gameWallet.address })
})

app.get('/get-balance', async (req, res) => {
  let uid = req.query.uid
  if (await db_game_wallets.has(uid)) {
    gameWallet = await db_game_wallets.get(uid)
  } else {
    return res.status(400).json({ "error": "wallet for user with uid doesn't exist" })
  }
  gameWalletCilUtils = new CilUtils({
    privateKey: gameWallet.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  const nBalance = await gameWalletCilUtils.getBalance();
  return res.status(200).json({ "balance": nBalance })
})

app.get('/refund-funds', async (req, res) => {
  let uid = req.query.uid
  console.log('Performing refunds for: ' + uid)
  if (await db_game_wallets.get(uid)) {
    gameWallet = await db_game_wallets.get(uid)
  }
  gameWalletCilUtils = new CilUtils({
    privateKey: gameWallet.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  const txList = await gameWalletCilUtils.getTXList();
  console.log(txList);
  for (let j = 0; j < txList.length; j++) {
    if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == gameWallet.address) {
      const balance = await gameWalletCilUtils.getBalance()
      console.log('Performing refund');
      console.log('Balance: ' + balance)
      console.log(txList[j]);
      console.log("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from)
      const txFunds = await gameWalletCilUtils.createSendCoinsTx([
        [txList[j].inputs[0].from, -1]], 0);
      await gameWalletCilUtils.sendTx(txFunds);
      await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      console.log('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from)
      break;
    }
  }
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

async function initGame(gameId, bid) {
  console.log('Game was put in DB ' + gameId)
  await db_game_statuses.put(gameId, [0, []]); // pending
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
  const gameWalletBalance = await gameWalletCilUtils.getBalance()
  console.log('Game wallet balance: ' + gameWalletBalance)

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
  //Estimate sum to send. If the Game Wallet balance is sufficient for withdrawing
  // both bid and commission - we withdraw both. It Game Wallet Balance > bid. but < bid + comission - we withdraw
  // (bid - commission) + comission = bid
  const arrUtxos = await gameWalletCilUtils.getUtxos();
  const txCost = gameWalletCilUtils._estimateTxFee(arrUtxos.length, 1, true) * 1.5;
  let sumToSend = 0
  if (bid + txCost > gameWalletBalance) {
    sumToSend = bid - txCost
  } else {
    sumToSend = bid
  }
  return { sumToSend }
}


async function updateParityListAndBalance(round, bid, currentBalance, gameId) {
  const tParityList = [];
  for (let i = 0; i < round; i++) {
    await randomDelay(round)
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
    console.log('Put game statuses to db')
    await db_game_statuses.put(
      gameId,
      [
        0,
        tParityList.slice(0, -1)
      ]); // pending
    console.log('Get game status')
    console.log(await db_game_statuses.get(gameId))
  }
  console.log(tParityList)
  return { currentBalance, tParityList }
}

async function startGame(round, bid, gameWalletKeyPair, gameId) {
  let tParityList = []
  try {
    const response1 = await initGame(gameId, bid)
    console.log('Sending funds from game wallet to transit wallet')
    const txFunds = await gameWalletCilUtils.createSendCoinsTx([
      [transitWalletKeyPair.address, response1.sumToSend]], 0);
    await gameWalletCilUtils.sendTx(txFunds);
    const response2 = await updateParityListAndBalance(round, bid, bid || 0, gameId)
    const response = [response1, response2];
    await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log(response1.sumToSend + ' UBX were sent to transit wallet')
    tParityList = response[1].tParityList
    if (response[1].currentBalance >= (bid || 0)) {
      // Send all funds from transit wallet to pool wallet
      console.log('Sending all funds from transit wallet to pool wallet')
      console.log('Transit wallet balance: ' + await transitWalletCilUtils.getBalance())
      console.log('Global pool wallet address: ' + global.poolWalletKeyPair.address)
      let txFunds = await transitWalletCilUtils.createSendCoinsTx([
        [global.poolWalletKeyPair.address, -1]], 0);
      await transitWalletCilUtils.sendTx(txFunds);
      await transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      console.log('All funds were sent from transit wallet to pool wallet')

      // estimate pool wallet to game wallet commission
      const poolWalletArrUtxos = await poolWalletCilUtils.getUtxos();
      const poolWalletTxCost = poolWalletCilUtils._estimateTxFee(poolWalletArrUtxos.length, 2, true);

      // Send CurrentBalance from pool wallet to game wallet
      // Send 2% of CurrentBalance from pool wallet to project Wallet
      // The user pays all commissions
      console.log('aaa: ' + response[1].currentBalance)
      console.log('aaa: ' + (response[1].currentBalance * 0.98 - poolWalletTxCost))
      txFunds = await global.poolWalletCilUtils.createSendCoinsTx(
        [
          [
            global.gameWalletKeyPair.address, (response[1].currentBalance * 0.98 - poolWalletTxCost)
          ],
          [
            global.projectWalletKeyPair.address, response[1].currentBalance * 0.02
          ],
        ],
        0
      );
      await global.poolWalletCilUtils.sendTx(txFunds);
      await global.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      console.log('CurrentBalance was sent from pool wallet to game wallet, 2% of CurrentBalance was sent from pool wallet to project Wallet')
    } else {
      //Estimate funds available for sending
      const arrUtxos = await transitWalletCilUtils.getUtxos();
      const txCost = transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
      const sumToSend = response[0].sumToSend - txCost;
      console.log(sumToSend)

      // Send from game wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
      console.log('Sending all funds from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet')
      const txFunds = await transitWalletCilUtils.createSendCoinsTx([
        [global.projectWalletKeyPair.address, sumToSend * 0.02],
        [global.poolWalletKeyPair.address, sumToSend * 0.784],
        [global.profitWalletKeyPair.address, sumToSend * 0.196]
      ], 0);
      await transitWalletCilUtils.sendTx(txFunds);
      await transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      console.log('From transit wallet funds were sent: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet')
    }
    let numberOfGamesInDb = await db_operations_log.get("numberOfGames");
    numberOfGamesInDb = Number(numberOfGamesInDb) + 1;
    await db_game_statuses.put(gameId, [1, tParityList]); // finished
    await db_operations_log.put("gameResult_" + numberOfGamesInDb, [gameWalletKeyPair, tParityList]);
    await db_operations_log.put("numberOfGames", numberOfGamesInDb);
    console.log('Game finished')
    return { success: true, parityList: tParityList }
  } catch (e) {
    console.error(e)
    await db_game_statuses.put(gameId, [1, tParityList]); // finished
  }
};

process.on('uncaughtException', err => {
  console.error(err);
});

app.listen(port, async () => {
  initCilUtils();
  const value = await db_operations_log.get("numberOfGames", (err) => {
    db_operations_log.put("numberOfGames", 1)
  })

  console.log(`RiskPool backend listening on port ${port}`);
  setInterval(performRefunds, 1000 * 60 * 60 * 24);
})
