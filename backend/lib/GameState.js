const {Level} = require("level");
const GameWallet = require("./GameWallet");

class GameState {
  _db
  constructor() {
    this._db = new Level(process.env.REFUNDS_DB_PATH, { valueEncoding: 'json' })
    this._db_operations_log = new Level(process.env.OPERATIONS_LOG_DB_PATH, { valueEncoding: 'json' })

  }

  async onStartGame (gameId) {
    await this._db_operations_log.put(gameId, [0, []]); // start
  }

  async onStartServer () {
    const value = await this._db_operations_log.get("numberOfGames", (err) => {
      this._db_operations_log.put("numberOfGames", 1)
    })

  }

  async performRefunds() {
    for await (const key of this._db.keys()) {
      let gameWallet = await this._db.get(key);
      if (!gameWallet.isRefunded) {
        let gameWalletCilUtils = new GameWallet(gameWallet.privateKey);
        await gameWalletCilUtils.performRefund(gameWallet.address)
        await this._db.del(key);
      }
    }
  }

  async saveWallet (address, privateKey, publicKey) {
    let gameWallet = {
      address,
      privateKey,
      publicKey,
      isRefunded: false,
    };
    await this._db.put("gameWallet_" + address, gameWallet);
  }
  async getGameStatus (gameId) {
    let value = await this._db_operations_log.get(gameId)
    console.log(value)
    if (value) {
      if (value[0]) {
        return { status: value[0], caption: "Game finished", parityList: value[1] }
      } else {
        return { status: value[0], caption: "Game in progress", parityList: value[1] }
      }
    } else {
      return { status: -1, caption: "Game doesn't exist", parityList: [] }
    }
  }

  async saveOnProcess (gameId, tParityList) {
      await this._db_operations_log.put(gameId, [0, tParityList]); // pending
  }

  async saveOnFinish (gameId, tParityList, gameWalletKeyPair, transitWalletKeyPair) {
    let numberOfGamesInDb = await this._db_operations_log.get("numberOfGames");
    numberOfGamesInDb = Number(numberOfGamesInDb) + 1;
    await this._db_operations_log.put(gameId, [1, tParityList]); // finished
    await this._db_operations_log.put("gameResult_" + numberOfGamesInDb, [gameWalletKeyPair, transitWalletKeyPair, tParityList]);
    await this._db_operations_log.put("numberOfGames", numberOfGamesInDb);
  }
}

module.exports = GameState
