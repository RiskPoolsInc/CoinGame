var sha256 = require('js-sha256').sha256;
var sha224 = require('js-sha256').sha224;
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


module.exports = {
    getRandomNumber,
    number2Hash,
    hash2Number,
    getId
}
