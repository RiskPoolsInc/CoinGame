var sha256 = require('js-sha256').sha256;
var sha224 = require('js-sha256').sha224;
const CilUtils = require('cil-utils');
var crypto = require("crypto");

function getId() {
    return crypto.randomBytes(20).toString('hex');
}

function getRandomNumber() {
    return Math.floor(Math.random() * 1000000);
};

function number2Hash(number) {
    return sha256(String(number));
};

function hash2Number(hash) {
    return sha224(hash);
};

function randomDelay(value) {
    return new Promise(resolve => setTimeout(resolve,  ( Math.random() *  ((1000 * 60 * 3)/value))));
}

const initCilUtils = async () => {

    global.poolWalletKeyPair = { "address": process.env.POOL_WALLET_ADDRESS, "privateKey": process.env.POOL_WALLET_PRIVATE_KEY, "publicKey": process.env.POOL_WALLET_PUBLIC_KEY }
    global.projectWalletKeyPair = { "address": process.env.PROJECT_WALLET_ADDRESS }
    global.profitWalletKeyPair = { "address": process.env.PROFIT_WALLET_ADDRESS }

    global.poolWalletCilUtils = new CilUtils({
        privateKey: global.poolWalletKeyPair.privateKey,
        apiUrl: process.env.CIL_UTILS_API_URL,
        rpcPort: process.env.CIL_UTILS_RPC_PORT,
        rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
        rpcUser: process.env.CIL_UTILS_RPC_USER,
        rpcPass: process.env.CIL_UTILS_RPC_PASS
    });
    await global.poolWalletCilUtils.asyncLoaded();
}

module.exports = {
    getRandomNumber,
    number2Hash,
    hash2Number,
    initCilUtils,
    getId,
    randomDelay
}
