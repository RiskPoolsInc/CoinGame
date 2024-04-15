var sha256 = require('js-sha256').sha256;
var sha224 = require('js-sha256').sha224;
const CilUtils = require('cil-utils');
var get = require('node-global-storage').get;
var set = require('node-global-storage').set;

function getRandomNumber() {
    return Math.floor(Math.random() * 1000000);
};

function number2Hash(number) {
    return sha256(String(number));
};

function hash2Number(hash) {
    return sha224(hash);
};

const initCilUtils = async () => {

    global.poolWalletKeyPair = { "address": "a317162377777dea68f05c88c8ff52362842a6df", "privateKey": "21df7a111c3452a3da0b4e051d9b1b3b68dc234e4f8c91c3bf30cb8f118eb7fa", "publicKey": "02c5db6110bab7653b17b0ef1412a2dd543a7d71a8e5c881c4bae7beb8343fc5d7" }
    global.projectWalletKeyPair = { "address": "8830e66a239bbd6682fb154df8b5bfc0e88088d1", "privateKey": "ec336b113b86bf2e0dd9a1ee91309bbec6b1e671b6c2209d47a2a0abcf495f82", "publicKey": "03347a8b16ca874f41c59d238b37ca64fb3305fb3e451d1f077b593bf9bdf770fa" }
    global.profitWalletKeyPair = { "address": "db6403750d902a40df2b90f7d781412094e3dc73", "privateKey": "3f8dbd33b002b585fb872c8c877f39c7dd1a1e6b386c0edfde3dafb72f45d6a3", "publicKey": "03bc6741e26d3e8cc03aff3a2b46fa7bf44d4b7a023c134d7a0c17edf1cfd1a9d8" }

    global.poolWalletCilUtils = new CilUtils({
        privateKey: global.poolWalletKeyPair.privateKey,
        apiUrl: 'https://test-explorer.ubikiri.com/api/',
        rpcPort: 443,
        rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
        rpcUser: 'cilTest',
        rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await global.poolWalletCilUtils.asyncLoaded();

    global.projectWalletCilUtils = new CilUtils({
        privateKey: global.projectWalletKeyPair.privateKey,
        apiUrl: 'https://test-explorer.ubikiri.com/api/',
        rpcPort: 443,
        rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
        rpcUser: 'cilTest',
        rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await global.projectWalletCilUtils.asyncLoaded();

    global.profitWalletCilUtils = new CilUtils({
        privateKey: global.profitWalletKeyPair.privateKey,
        apiUrl: 'https://test-explorer.ubikiri.com/api/',
        rpcPort: 443,
        rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
        rpcUser: 'cilTest',
        rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await global.profitWalletCilUtils.asyncLoaded();
}

module.exports = {
    getRandomNumber,
    number2Hash,
    hash2Number,
    initCilUtils
}