const storage = require('node-persist');
const crypto = require('crypto-web');
const CilUtils = require('cil-utils');
const express = require('express');
const app = express();
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('./openapi.json');
const port = 3000

app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));

app.get('/', (req, res) => {
  res.send('Hello World!')
})

app.get('/upload-game-wallet', async (req, res) => {
    let address = req.query.address;
    let privateKey = req.query.privateKey;
    let publicKey = req.query.publicKey;
    let gameWallets = await storage.getItem("gameWallets");
    gameWallets.push({
        address: address,
        privateKey: privateKey,
        publicKey: publicKey,
    });
    await storage.setItem("gameWallets", gameWallets);
    console.log('Wallet')
    console.log(await storage.getItem("gameWallets"))
    performRefunds();
    return { code: 200 };
})

async function performRefunds() {
    console.log('Refunds:')
    let gameWallets = await storage.getItem("gameWallets");
    for (let i = 0; i < gameWallets.length; i++) {
        let gameWalletCilUtils = new CilUtils({
            privateKey: gameWallets[i].privateKey,
            apiUrl: 'https://test-explorer.ubikiri.com/api/',
            rpcPort: 443,
            rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
            rpcUser: 'cilTest',
            rpcPass: 'd49c1d2735536baa4de1cc6'
        });
        await gameWalletCilUtils.asyncLoaded();   
        const txList = await gameWalletCilUtils.getTXList();
        for (j = 0; j < txList.length; j++) {
            if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == gameWallets[i].address) {
                console.log('Performing refund');
                const arrUtxos = await gameWalletCilUtils.getUtxos();
                const txCost = gameWalletCilUtils._estimateTxFee(arrUtxos.length, 1, true);
                const sumToSend = txList[j].outputs[0].amount - txCost;
                const txFunds = await gameWalletCilUtils.createSendCoinsTx([
                    [txList[j].inputs[0].from, sumToSend]
                  ], 0);
                await gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
            }
        }
    }
}

app.listen(port, async () => {
    await storage.init( /* options ... */ );
    let gameWallets = await storage.getItem('gameWallets');
    if (!gameWallets) {
        await storage.setItem("gameWallets", []);
    }
    console.log(`Example app listening on port ${port}`);
})