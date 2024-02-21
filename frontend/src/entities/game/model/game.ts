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
    poolWalletKeyPair: null,
    profitWalletKeyPair: null,
    projectWalletKeyPair: null,
    gameWalletCilUtils: null,
    transitWalletCilUtils: null,
    poolWalletCilUtils: null,
    profitWalletCilUtils: null,
    projectWalletCilUtils: null,
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

    gameState.poolWalletCilUtils = new CilUtils({
      privateKey: gameState.poolWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.poolWalletCilUtils.asyncLoaded();

    gameState.projectWalletCilUtils = new CilUtils({
      privateKey: gameState.profitWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.poolWalletCilUtils.asyncLoaded();
  }

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const generateWallet = async () => {
    // gameState.gameWalletKeyPair = crypto.createKeyPair();
    gameState.gameWalletKeyPair = {"address": "a4f474b2ccbe1875d2ba6389c494e912e744cbf2", "privateKey": "17c2e3bf92a5369298fb7f7e4c709990576b4ff9d7f166bb428cc92719c2a6be", "publicKey": "02ad01bb66eea6ff3e717f835cd900ab97e798ce3f2797c614734df9257e2f030f"}
    gameState.wallet = 'Ux' + gameState.gameWalletKeyPair.address;
    gameState.transitWalletKeyPair = crypto.createKeyPair();
    // gameState.poolWalletKeyPair = crypto.createKeyPair();
    gameState.poolWalletKeyPair = {"address": "a317162377777dea68f05c88c8ff52362842a6df", "privateKey": "21df7a111c3452a3da0b4e051d9b1b3b68dc234e4f8c91c3bf30cb8f118eb7fa", "publicKey": "02c5db6110bab7653b17b0ef1412a2dd543a7d71a8e5c881c4bae7beb8343fc5d7"}
    gameState.projectWalletKeyPair = {"address": "8830e66a239bbd6682fb154df8b5bfc0e88088d1", "privateKey": "ec336b113b86bf2e0dd9a1ee91309bbec6b1e671b6c2209d47a2a0abcf495f82", "publicKey": "03347a8b16ca874f41c59d238b37ca64fb3305fb3e451d1f077b593bf9bdf770fa"}
    gameState.profitWalletKeyPair = {"address": "db6403750d902a40df2b90f7d781412094e3dc73", "privateKey": "3f8dbd33b002b585fb872c8c877f39c7dd1a1e6b386c0edfde3dafb72f45d6a3", "publicKey": "03bc6741e26d3e8cc03aff3a2b46fa7bf44d4b7a023c134d7a0c17edf1cfd1a9d8"}

    await initCilUtils();
    const arrUtxos = await gameState.gameWalletCilUtils.getUtxos();
    console.log(arrUtxos);
    const nBalance = await gameState.gameWalletCilUtils.getBalance();
    console.log("Game wallet balance:" +nBalance);
    const txCost = gameState.transitWalletCilUtils._estimateTxFee(nBalance, 3, true);
    console.log(txCost)
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

    const txFunds = await gameState.gameWalletCilUtils.createSendCoinsTx([
      [gameState.transitWalletKeyPair.address, gameState.bid]], 0);
    // Не забыть отправить ее в сеть
    console.log("sending funds to transit wallet step 1...");
    await gameState.gameWalletCilUtils.sendTx(txFunds);
    console.log("sending funds to transit wallet step 2...");
    // Дождаться ее выполнения
    await gameState.gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    console.log("Funds were sent to transit wallet");
    const nBalance = await gameState.transitWalletCilUtils.getBalance();
    console.log("Transit wallet balance:" + nBalance);

    console.log(gameState.round)
    for (let i = 0; i < gameState.round; i++) {
      console.log('round' + i);
      const randomNumber = getRandomNumber();
      const hash = number2Hash(randomNumber);
      const parity = randomNumber % 2 !== 0;
      console.log(parity);
      if (parity) {
        currentBalance = Number(currentBalance) + Number(gameState.bid);
      } else {
        if (Number(currentBalance) - Number(gameState.bid) < 0) { return; }
        else {
          currentBalance = Number(currentBalance) - Number(gameState.bid);
        }
      }
      console.log('Balance calc')

      balanceCalculation(parity);

      gameState.parityList.push({
        round: i,
        number: randomNumber,
        parity: parity,
        currentBalance: currentBalance,
        hashNumber: hash,
      });
    }
    if (currentBalance >= gameState.bid) {
      console.log("Current balance (win): " + currentBalance);
      console.log("Bid: " + gameState.bid);
      let txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
        [gameState.poolWalletKeyPair.address, -1]], 0); // Send all funds
      // Не забыть отправить ее в сеть
      await gameState.transitWalletCilUtils.sendTx(txFunds);
      console.log('Sending all funds to pool wallet')
    
      // Дождаться ее выполнения
      await gameState.transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      let nBalance = await gameState.poolWalletCilUtils.getBalance();
      console.log("Funds sent to pool wallet. Pool wallet balance: " + nBalance);

      // Move CurrentBalance from pool to Player

      txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
        [gameState.gameWalletKeyPair.address, currentBalance]], 0);
      // Не забыть отправить ее в сеть
      await gameState.poolWalletCilUtils.sendTx(txFunds);
      console.log('Sending ' + currentBalance + ' from pool')
    
      // Дождаться ее выполнения
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      nBalance = await gameState.gameWalletCilUtils.getBalance();
      console.log("Game wallet balance: " + nBalance);

      // Move 2% of CurrentBalance from pool to Project Wallet

      txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
        [gameState.projectWalletKeyPair.address, currentBalance * 0.02]], 0);
      // Не забыть отправить ее в сеть
      await gameState.poolWalletCilUtils.sendTx(txFunds);
      console.log('Sending ' + currentBalance * 0.02 + ' from pool')
    
      // Дождаться ее выполнения
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      nBalance = await gameState.projectWalletCilUtils.getBalance();
      console.log("Project wallet balance: " + nBalance);

    } else {
      console.log("Current balance (lose): " + currentBalance);
      const arrUtxos = await gameState.transitWalletCilUtils.getUtxos();
      const walletBalance = arrUtxos.reduce((accum: any, current: any) => accum + current.amount, 0);
      console.log(arrUtxos);
      console.log(walletBalance);
      const txCost = gameState.transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
      console.log(txCost);
      const sumToSend = walletBalance - txCost;
      console.log(sumToSend);
      
      console.log('Sending money (lose)')
      const txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
        [gameState.projectWalletKeyPair.address, sumToSend * 0.02],
        [gameState.profitWalletKeyPair.address, sumToSend * 0.784],
        [gameState.poolWalletKeyPair.address, sumToSend * 0.196]
      ], 0);
      await gameState.transitWalletCilUtils.sendTx(txFunds);
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      const nBalance = await gameState.projectWalletCilUtils.getBalance();
      console.log("Game wallet balance: " + nBalance);
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
