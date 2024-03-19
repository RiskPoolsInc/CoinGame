const storage = require('node-persist');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const app = express();
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000
var cron = require('node-cron');

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
    };
    await storage.setItem("gameWallet_" + address, gameWallet);

    // performRefunds();
    cron.schedule('* * 24 * *', () => {
        performRefund(address);
    })
    return res.status(200).json({ success: true })
})

async function performRefund(walletAddress) {
    console.log('Performing refunds for wallet: ' + walletAddress);
    let gameWallet = await storage.getItem("gameWallet_" + walletAddress);
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
            await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
            console.log('Refunded ' + sumToSend + ' UBX to: ' + txList[j].inputs[0].from)
        }
    }
}

app.listen(port, async () => {
    await storage.init( /* options ... */ );
    console.log(`Example app listening on port ${port}`);
})