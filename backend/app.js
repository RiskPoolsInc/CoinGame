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
    console.log(await storage.getItem("gameWallets"))
    // gameWalletCilUtils = new CilUtils({
    //     privateKey: privateKey,
    //     apiUrl: 'https://test-explorer.ubikiri.com/api/',
    //     rpcPort: 443,
    //     rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
    //     rpcUser: 'cilTest',
    //     rpcPass: 'd49c1d2735536baa4de1cc6'
    //   });
    //   await gameWalletCilUtils.asyncLoaded();
    return { code: 200 };
})

app.listen(port, async () => {
    await storage.init( /* options ... */ );
    let gameWallets = await storage.getItem('gameWallets');
    if (!gameWallets) {
        await storage.setItem("gameWallets", []);
    }
    console.log(`Example app listening on port ${port}`);
})