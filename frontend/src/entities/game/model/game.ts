// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');
// eslint-disable-next-line @typescript-eslint/no-var-requires
const CilUtils = require('cil-utils');

import {defineStore} from "pinia";
import {reactive} from "vue";
import {sha224, sha256} from 'js-sha256';

import {IGameState} from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    wallet: "",
    balance: 0,
    previousBalance: 0,
    bid: 0,
    round: 3,
    parityList: [],
    gameWalletKeyPair: null,
    transitWalletKeyPair: null,
    gameWalletCilUtils: null,
    transitWalletCilUtils: null,
  });

  const initCilUtils = async () => {
    gameState.gameWalletCilUtils = new CilUtils({
      privateKey: gameState.gameWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.gameWalletCilUtils.asyncLoaded();

    gameState.transitWalletCilUtils = new CilUtils({
      privateKey: gameState.transitWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.transitWalletCilUtils.asyncLoaded();
  }

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const generateWallet = async () => {
    gameState.gameWalletKeyPair = crypto.createKeyPair();
    gameState.wallet = 'Ux' + gameState.gameWalletKeyPair.address;
    gameState.transitWalletKeyPair = crypto.createKeyPair();


    await initCilUtils();   
    console.log(gameState.gameWalletKeyPair) 

    console.log(gameState.transitWalletKeyPair) 
    const nBalance = await gameState.gameWalletCilUtils.getBalance();
    console.log(nBalance);
    gameState.balance = nBalance // + 1000000; // TEMP!   
  };

  const copyWallet = () => {
    navigator.clipboard.writeText(gameState.wallet);
  };

  const number2Hash = (number: number) => {
    return sha256(String(number));
  };

  const hash2Number = (hash: string) => {
    return sha224(hash);
  };

  const getRandomNumber = (): number => {
    return Math.floor(Math.random() * 1000000);
  };

  const balanceCalculation = (parity: boolean) => {
    if (parity) {
      gameState.balance += Number(gameState.bid);
    } else {
      gameState.balance -= Number(gameState.bid);
    }
  };

  const resetGame = () => {
    gameState.previousBalance = gameState.balance;
    gameState.parityList = [];
  };

  const generalReset = async () => {
    gameState.balance = 1000000;   
    gameState.previousBalance = 0;
    gameState.bid = 0;
    gameState.round = 3;
    gameState.parityList = [];
  };

  const startGame = async () => {
    if (gameState.parityList.length > 0) {
      resetGame();
    }
    gameState.previousBalance = gameState.balance;

    let currentBalance = gameState.bid;

    for (let i = 0; i < gameState.round; i++) {
      console.log('round');
      const randomNumber = getRandomNumber();
      const hash = number2Hash(randomNumber);
      const parity = randomNumber % 2 !== 0;

      const txFunds = await gameState.gameWalletCilUtils.createSendCoinsTx([
        [gameState.transitWalletKeyPair.address, gameState.bid]], 0);
      // Не забыть отправить ее в сеть
      await gameState.gameWalletCilUtils.sendTx(txFunds);
    
      // Дождаться ее выполнения
      await gameState.gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      const nBalance = await gameState.transitWalletCilUtils.getBalance();
      console.log(nBalance);
      

      if (currentBalance <= 0) {
        return;
      }

      if (parity) {
        currentBalance = Number(currentBalance) + Number(gameState.bid);
      } else {
        if (Number(currentBalance) - Number(gameState.bid) < 0) { return; }
        else {
          currentBalance = Number(currentBalance) - Number(gameState.bid);
        }
      }

      balanceCalculation(parity);

      gameState.parityList.push({
        round: i,
        number: randomNumber,
        parity: parity,
        currentBalance: currentBalance,
        hashNumber: hash,
      });
    }
  };

  return {
    gameState,
    setBid,
    generalReset,
    number2Hash,
    hash2Number,
    startGame,
    copyWallet,
    generateWallet,
  };
});
