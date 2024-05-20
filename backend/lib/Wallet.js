const CilUtils = require('cil-utils');

class Wallet extends CilUtils {
  constructor (privateKey) {
    super({
      privateKey,
      apiUrl: process.env.CIL_UTILS_API_URL,
      rpcPort: process.env.CIL_UTILS_RPC_PORT,
      rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
      rpcUser: process.env.CIL_UTILS_RPC_USER,
      rpcPass: process.env.CIL_UTILS_RPC_PASS,
    });
  }
  async send (receivers) {
    const txFunds = await this.createSendCoinsTx(receivers, 0)
    await this.sendTx(txFunds)
    await this.waitTxDoneExplorer(txFunds.getHash())
  }
  get address () {
    return this._kpFunds.address
  }
}

module.exports = Wallet
