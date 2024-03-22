const { Level } = require('level');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const cors = require('cors');
const app = express();
const corsOptions = {
    origin: '*',
    optionsSuccessStatus: 200,
  };
 
  app.use(cors(corsOptions));
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000
var cron = require('node-cron');
const db = new Level('refunds', { valueEncoding: 'json' })

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

async function performRefunds() {
    for await (const key of db.keys()) {
        let gameWallet = await db.get(key);
        if (! gameWallet.isRefunded) {
            await performRefund(gameWallet);
            db.del(key);
        }
      }
}

async function performRefund(gameWallet) {
    console.log('Performing refunds for wallet: ' + gameWallet.address);
    let gameWalletCilUtils = new CilUtils({
        privateKey: gameWallet.privateKey,
        apiUrl: 'https://test-explorer.ubikiri.com/api/',
        rpcPort: 443,
        rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
        rpcUser: 'cilTest',
        rpcPass: 'd49c1d2735536baa4de1cc6'
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

app.listen(port, async () => {
    console.log(`Example app listening on port ${port}`);
    setInterval(performRefunds, 1000 * 60 * 60 * 24);
})