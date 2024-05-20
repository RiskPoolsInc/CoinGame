const Wallet = require("./Wallet");

class PoolWallet extends Wallet {
  constructor() {
    super(process.env.POOL_WALLET_PRIVATE_KEY);
  }
  async sendToGameAndProject (value, gameAddress) {
    await this.send([
      [gameAddress, value],
      [process.env.PROJECT_WALLET_ADDRESS, value * 0.02]
    ]);
  }
}

module.exports = PoolWallet
