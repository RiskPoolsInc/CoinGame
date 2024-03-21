const { Level } = require('level');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const app = express();
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
            console.log('Performing refund');
            console.log(txList[j]);
            const arrUtxos = await gameWalletCilUtils.getUtxos();
            const txCost = gameWalletCilUtils._estimateTxFee(arrUtxos.length, 1, true);
            const sumToSend = txList[j].outputs[0].amount - txCost;
            const txFunds = await gameWalletCilUtils.createSendCoinsTx([
                [txList[j].inputs[0].from, sumToSend]
                ], 0);
            console.log('Transaction in progress')
            await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
            console.log('Refunded ' + sumToSend + ' UBX to: ' + txList[j].inputs[0].from)
        }
    }
}

app.listen(port, async () => {
    // let t = crypto.createKeyPair();
    // console.log(t.address);
    // console.log(t.privateKey);
    // console.log(t.publicKey)
    let gameWalletCilUtils = new CilUtils({
        privateKey: "c833d9950b328984d9484b6314c659108bf125ee30a2d4c888e933f52bb3bcef",
        apiUrl: 'https://test-explorer.ubikiri.com/api/',
        rpcPort: 443,
        rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
        rpcUser: 'cilTest',
        rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    console.log(await gameWalletCilUtils.getBalance())
    // const txFunds = await gameWalletCilUtils.createSendCoinsTx([
    //     ["07f015ba6162223fee5891310b264615d28e3865", 1000]
    //   ], 0);
    // console.log('aaa')
    // await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());



    console.log(`Example app listening on port ${port}`);
    performRefunds();
})