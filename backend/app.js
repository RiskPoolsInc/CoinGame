const dotenv = require("dotenv")
const envFile = process.env.NODE_ENV ? `.env.${process.env.NODE_ENV}` : '.env.development'
dotenv.config({ path: envFile })
const { Level } = require('level');
const cors = require('cors');
const winston = require('winston');
require('winston-daily-rotate-file');
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const { getId, getRandomNumber, number2Hash, hash2Number, initCilUtils, randomDelay, dbHas } = require('./helpers.js')
const express = require('express');
const app = express();
const port = process.env.SERVER_PORT || 3000
app.use(cors());
var encrypter = require('object-encrypter');
var engine = encrypter(process.env.DB_SECRET);

const db = new Level(process.env.REFUNDS_DB_PATH, { valueEncoding: 'json' })
const db_game_statuses = new Level(process.env.GAME_STATUSES_DB_PATH, { valueEncoding: 'json' })
const db_game_logs = new Level(process.env.OPERATIONS_LOG_DB_PATH, { valueEncoding: 'json' })
const db_game_wallets = new Level(process.env.GAME_WALLETS_DB_PATH, { valueEncoding: 'json' })
const db_transit_wallets = new Level(process.env.TRANSIT_WALLETS_DB_PATH, { valueEncoding: 'json' })
db.has = dbHas
db_game_wallets.has = db.has
db_game_statuses.has = db.has
db_transit_wallets.has = db.has
db_game_logs.has = db.has

var monthlyLogTransport = new winston.transports.DailyRotateFile({
  filename: 'application-%DATE%.log',
  datePattern: 'YYYY-MM-DD-HH',
  zippedArchive: true,
  maxSize: '100m',
  maxFiles: '30d'
});

var monthlyExceptionsTransport = new winston.transports.DailyRotateFile({
  filename: 'errors-%DATE%.log',
  datePattern: 'YYYY-MM-DD-HH',
  zippedArchive: true,
  maxSize: '100m',
  maxFiles: '30d',
  level: 'error'
});

const logger = winston.createLogger({
  format: winston.format.combine(
    winston.format.timestamp({
      format: 'YYYY-MM-DD HH:mm:ss'
    }),
    winston.format.errors({ stack: true }),
    winston.format.splat(),
    winston.format.json()
  ),
  transports: [
    new winston.transports.Console(),
    monthlyLogTransport,
    monthlyExceptionsTransport
  ],
});

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

app.get('/', (req, res) => {
  res.send('This is RiskPools backend. Docs: /api-docs')
})

app.get('/play-game', async (req, res) => {
  try {
    let round = req.query.round;
    let bid = Number(req.query.bid);
    let uid = req.query.uid
    let gameId = getId();
    logger.info(`==================================================================`, { gameId: gameId, uid: uid })
    logger.info(`Playing game for user ${uid}. Rounds = ${round}. Bid = ${bid} UBX.`, { gameId: gameId, uid: uid })
    if (bid < Number(process.env.MIN_BID) || bid > Number(process.env.MAX_BID)) {
      logger.warn(`Incorrect bid amount. MIN_BID =  ${process.env.MIN_BID}. MAX_BID = ${process.env.MAX_BID}`, { gameId: gameId, uid: uid })
      res.status(400).json({ error: "Incorrect bid amount" })
    }
    if (round < Number(process.env.MIN_ROUND) || (round > Number(process.env.MAX_ROUND))) {
      logger.warn(`Incorrect rounds amount. MIN_ROUND =  ${process.env.MIN_ROUND}. MAX_ROUND = ${process.env.MAX_ROUND}`, { gameId: gameId, uid: uid })
      res.status(400).json({ error: "Incorrect rounds amount" })
    }
    gameWallet = engine.decrypt(await db_game_wallets.get(uid))
    gameWalletKeyPair = {
      address: gameWallet.address,
      privateKey: gameWallet.privateKey,
      publicKey: gameWallet.publicKey,
    }
    logger.info('Game ID: ' + gameId, { gameId: gameId, uid: uid });
    startGame(round, bid, gameWalletKeyPair, gameId, uid) // this is async, and we don't wait for finish execution
    return res.status(200).json({ gameid: gameId })
  } catch (e) {
    console.error(e)
    logger.error(JSON.stringify(e))
    return res.status(501).json({ error: "The game ended unexpectedly" })
  }
})

app.get('/game-status', async (req, res) => {
  let gameId = req.query.gameid;
  let gameCheck = null
  let value = engine.decrypt(await db_game_statuses.get(gameId)).status
  if (value) {
    if (value[0]) {
      gameCheck = { status: value[0], caption: "Game finished", parityList: value[1] }
      logger.info('Checking game status: return Game Finished' + req.query.gameid, { gameId: gameId })
    } else {
      gameCheck = { status: value[0], caption: "Game in progress", parityList: value[1] }
    }
  } else {
    gameCheck = { status: -1, caption: "Game doesn't exist", parityList: null }
    logger.warn('Checking game status: return Game Doesnt exist' + req.query.gameid, { gameId: gameId })
  }
  return res.status(200).json(gameCheck)
})

app.get('/create-game-wallet', async (req, res) => {
  let uid = req.query.uid
  logger.info('Creating game wallet for user: ' + uid, { uid: uid })
  let gameWallet = {}
  isRestored = true
  if (await db_game_wallets.has(uid)) {
    gameWallet = await engine.decrypt(await db_game_wallets.get(uid))
  } else {
    logger.info('Wallet for uid not found', { uid: uid })
    gameWalletKeyPair = crypto.createKeyPair();
    gameWallet = {
      address: gameWalletKeyPair.address,
      privateKey: gameWalletKeyPair.privateKey,
      publicKey: gameWalletKeyPair.publicKey,
    }
    db_game_wallets.put(uid, engine.encrypt(gameWallet))
    isRestored = false
  }
  logger.info("Address: " + gameWallet.address, { uid: uid })
  return res.status(200).json({ "isRestored": isRestored, "address": gameWallet.address })
})

app.get('/get-balance', async (req, res) => {
  let uid = req.query.uid
  if (await db_game_wallets.has(uid)) {
    gameWallet = await engine.decrypt(await db_game_wallets.get(uid))
  } else {
    logger.warn("wallet for user with uid doesn't exist", { uid: uid })
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
  try {
    let uid = req.query.uid
    logger.info('Performing refunds for: ' + uid, { uid: uid })
    if (await db_game_wallets.get(uid)) {
      gameWallet = engine.decrypt(await db_game_wallets.get(uid))
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
    logger.info(txList);
    for (let j = 0; j < txList.length; j++) {
      if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == gameWallet.address) {
        const balance = await gameWalletCilUtils.getBalance()
        logger.info('Performing refund', { uid: uid });
        logger.info('Balance: ' + balance, { uid: uid })
        logger.info(txList[j], { uid: uid });
        logger.info("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from, { uid: uid })
        const txFunds = await gameWalletCilUtils.createSendCoinsTx([
          [txList[j].inputs[0].from, -1]], 0);
        await gameWalletCilUtils.sendTx(txFunds);
        await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
        logger.info('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from, { uid: uid })
        break;
      }
    }
    return res.status(200).json({ "success": "true" })
  } catch (e) {
    return res.status(400).json({ "error": e })
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
  logger.info('Performing refunds for wallet: ' + gameWallet.address);
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
      logger.info('Performing refund');
      logger.info('Balance: ' + balance)
      logger.info(txList[j]);
      logger.info("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from)
      let txFunds = await gameWalletCilUtils.createSendCoinsTx([
        [txList[j].inputs[0].from, -1]], 0);
      await gameWalletCilUtils.sendTx(txFunds);
      await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      logger.info('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from)
    }
  }
}

async function initGame(gameId, bid) {
  await db_game_statuses.put(gameId, engine.encrypt({ status: [0, []] })); // pending
  logger.info('Game was put in DB: ' + gameId, { gameId: gameId })
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
  db_game_wallets.put(gameId, engine.encrypt(gameWalletKeyPair))
  const gameWalletBalance = await gameWalletCilUtils.getBalance()
  logger.info('Game wallet was opened. Balance: ' + gameWalletBalance, { gameId: gameId })

  transitWalletKeyPair = crypto.createKeyPair();
  db_transit_wallets.put(gameId, engine.encrypt(transitWalletKeyPair))
  transitWalletCilUtils = new CilUtils({
    privateKey: transitWalletKeyPair.privateKey,
    apiUrl: process.env.CIL_UTILS_API_URL,
    rpcPort: process.env.CIL_UTILS_RPC_PORT,
    rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
    rpcUser: process.env.CIL_UTILS_RPC_USER,
    rpcPass: process.env.CIL_UTILS_RPC_PASS
  });
  await transitWalletCilUtils.asyncLoaded();
  logger.info('Transit wallet was opened.', { gameId: gameId })

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
  logger.info(`Sum to send from game wallet to transit wallet: ${sumToSend}`, { gameId: gameId })
  return { sumToSend }
}


async function updateParityListAndBalance(round, bid, currentBalance, gameId) {
  const tParityList = [];
  for (let i = 0; i < round; i++) {
    await randomDelay(round)
    logger.info('Playing round: ' + i, { gameId: gameId });
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
    logger.info('Put game statuses to db', { gameId: gameId })
    await db_game_statuses.put(
      gameId,
      engine.encrypt({
        status: [
          0,
          tParityList.slice(0, -1)
        ]
      })); // pending
  }
  logger.info('Parity list:' + JSON.stringify(tParityList), { gameId: gameId })
  return { currentBalance, tParityList }
}

async function startGame(round, bid, gameWalletKeyPair, gameId, uid) {
  let tParityList = []
  try {
    const response1 = await initGame(gameId, bid)
    logger.info(`Sending funds from game wallet to transit wallet. Transit wallet address: ${transitWalletKeyPair.address}. Sum to send: ${response1.sumToSend} `, { gameId: gameId })
    const txFunds = await gameWalletCilUtils.createSendCoinsTx([
      [transitWalletKeyPair.address, response1.sumToSend]], 0);
    await gameWalletCilUtils.sendTx(txFunds);
    const response2 = await updateParityListAndBalance(round, bid, bid || 0, gameId)
    const response = [response1, response2];
    await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    logger.info(response1.sumToSend + ' UBX were sent from game wallet to transit wallet', { gameId: gameId })
    tParityList = response[1].tParityList
    if (response[1].currentBalance >= (bid || 0)) {
      // Send all funds from transit wallet to pool wallet
      logger.info('Sending all funds from transit wallet to pool wallet', { gameId: gameId })
      logger.info('Transit wallet balance: ' + await transitWalletCilUtils.getBalance(), { gameId: gameId })
      logger.info('Global pool wallet address: ' + global.poolWalletKeyPair.address, { gameId: gameId })
      let txFunds = await transitWalletCilUtils.createSendCoinsTx([
        [global.poolWalletKeyPair.address, -1]], 0);
      await transitWalletCilUtils.sendTx(txFunds);
      await transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      logger.info('All funds were sent from transit wallet to pool wallet', { gameId: gameId })

      // estimate pool wallet to game wallet commission
      const poolWalletArrUtxos = await poolWalletCilUtils.getUtxos();
      const poolWalletTxCost = poolWalletCilUtils._estimateTxFee(poolWalletArrUtxos.length, 2, true);

      // Send CurrentBalance from pool wallet to game wallet
      // Send 2% of CurrentBalance from pool wallet to project Wallet
      // The user pays all commissions
      logger.info('Sending CurrentBalance of ' + response[1].currentBalance + 'UBX from pool wallet to game wallet. Sending 2% of CurrentBalance from pool wallet to project Wallet. Comissions are: ' + poolWalletTxCost + ' UBX', { gameId: gameId })
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
      logger.info('CurrentBalance was sent from pool wallet to game wallet, 2% of CurrentBalance was sent from pool wallet to project Wallet', { gameId: gameId })
    } else {
      //Estimate funds available for sending
      const arrUtxos = await transitWalletCilUtils.getUtxos();
      const txCost = transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
      const sumToSend = response[0].sumToSend - txCost;

      // Send from game wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
      logger.info(`Sending all ${sumToSend} UBX funds from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet`, { gameId: gameId })
      const txFunds = await transitWalletCilUtils.createSendCoinsTx([
        [global.projectWalletKeyPair.address, sumToSend * 0.02],
        [global.poolWalletKeyPair.address, sumToSend * 0.784],
        [global.profitWalletKeyPair.address, sumToSend * 0.196]
      ], 0);
      await transitWalletCilUtils.sendTx(txFunds);
      await transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      logger.info('From transit wallet funds were sent: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet', { gameId: gameId })
    }
    await db_game_statuses.put(gameId, engine.encrypt({ status: [1, tParityList] })); // finished
    await db_game_logs.put(gameId, engine.encrypt({
      parityList: tParityList,
      uid: uid,
      gameWalletKeyPair: gameWalletKeyPair,
      transitWalletKeyPair: transitWalletKeyPair
    })); // finished
    logger.info('Game finished', { gameId: gameId })
    return { success: true, parityList: tParityList }
  } catch (e) {
    console.error(e)
    logger.error(JSON.stringify(e))
    await db_game_statuses.put(gameId, engine.encrypt({ status: [1, tParityList] })); // finished
  }
};

process.on('uncaughtException', err => {
  console.error(err);
  logger.error(JSON.stringify(err))
});

app.listen(port, async () => {
  initCilUtils();

  console.log(`RiskPool backend listening on port ${port}`);
  setInterval(performRefunds, 1000 * 60 * 60 * 24);
})
