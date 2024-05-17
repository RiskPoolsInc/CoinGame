const Wallet = require('./Wallet')
const crypto = require("crypto-web");
class TransitWallet extends Wallet {
  //Estimate funds available for sending
  _keypair
  constructor () {
    const keyPair = crypto.createKeyPair();
    super(keyPair.privateKey)
    this._keypair = keyPair
  }
  async sumToSend () {
    const arrUtxos = await this.getUtxos();
    const walletBalance = arrUtxos.reduce((accum, current) => accum + current.amount, 0);
    const txCost = this._estimateTxFee(arrUtxos.length, 3, true);
    return  walletBalance - txCost;
  }
  // Send from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
  async sendToPoolProjectAndProfit () {
    const value = await this.sumToSend()
    await this.send([
      [process.env.PROJECT_WALLET_ADDRESS, value * 0.02],
      [process.env.POOL_WALLET_ADDRESS, value * 0.784],
      [process.env.PROFIT_WALLET_ADDRESS, value * 0.196]
    ]);
  }
  async sendToPool () {
    await this.send([
      [process.env.POOL_WALLET_ADDRESS, -1]])
  }
  get keypair () {
    return this._keypair
  }
}

module.exports = TransitWallet
