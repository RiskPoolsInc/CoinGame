const Wallet = require('./Wallet')
class GameWallet extends Wallet {
  async sumToSend (bid) {
    const arrUtxos = await this.getUtxos();
    const txCost = this._estimateTxFee(arrUtxos.length, 1, true) * 1.5;
    if (bid + txCost > this.getBalance) {
      return  bid - txCost
    } else {
      return  bid
    }
  }
  async sendToTransit (bid, address) {
    const value = await this.sumToSend(bid)
    await this.send([[address, value]])
  }
  async performRefund (address) {
    const txList = await this.getTXList();
    for (let j = 0; j < txList.length; j++) {
      if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == address) {
        const balance = await this.getBalance()
        console.log('Performing refund');
        console.log('Balance: ' + balance)
        console.log(txList[j]);
        console.log("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from)
        await this.send([
          [txList[j].inputs[0].from, -1]]
        )
        console.log('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from)
      }
    }
  }
}

module.exports = GameWallet
