// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');
// eslint-disable-next-line @typescript-eslint/no-var-requires
const CilUtils = require('cil-utils');
import axios from "axios";
const api_backend = axios.create({ baseURL: "http://localhost:3000" })

import {defineStore} from "pinia";
import {reactive} from "vue";
import {sha224, sha256} from 'js-sha256';
import { useCookies } from '@vueuse/integrations/useCookies'

const cookies = useCookies()

import {IGameState} from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    wallet: "",
    balance: 0,
    previousBalance: 0,
    bid: null,
    bidForBalanceChart: 0,
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
      privateKey: gameState.projectWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.projectWalletCilUtils.asyncLoaded();

    gameState.profitWalletCilUtils = new CilUtils({
      privateKey: gameState.profitWalletKeyPair.privateKey,
      apiUrl: 'https://test-explorer.ubikiri.com/api/',
      rpcPort: 443,
      rpcAddress: 'https://rpc-dv-1.ubikiri.com/',
      rpcUser: 'cilTest',
      rpcPass: 'd49c1d2735536baa4de1cc6'
    });
    await gameState.profitWalletCilUtils.asyncLoaded();
  }

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const restoreWallet = async() => {
    if (cookies.get('gameWalletKeyPair')) {
      generateWallet(true);
    }
  }

  const updateBalance = async() => {
    const nBalance = await gameState.gameWalletCilUtils.getBalance();
    console.log(nBalance)
    gameState.balance = nBalance; 
  }

  const generateWallet = async (restoreWallet = false) => {
    if (restoreWallet) {
      gameState.gameWalletKeyPair = cookies.get('gameWalletKeyPair');
    } else {
      gameState.gameWalletKeyPair = crypto.createKeyPair();
    }
    cookies.set('gameWalletKeyPair', {
      address: gameState.gameWalletKeyPair.address,
      privateKey: gameState.gameWalletKeyPair.privateKey,
      publicKey: gameState.gameWalletKeyPair.publicKey,
    });
    const res = await api_backend.get('upload-game-wallet'+'?address='+encodeURIComponent(gameState.gameWalletKeyPair.address)+'&privateKey='+encodeURIComponent(gameState.gameWalletKeyPair.privateKey)+'&publicKey='+encodeURIComponent(gameState.gameWalletKeyPair.publicKey), {
      headers: {
        Origin: 'http://localhost:8080',
      }
    })
      .then((response) => {
          console.log(response.data);
      })
      .catch((error) => {
          console.log(error)
      })


    gameState.wallet = 'Ux' + gameState.gameWalletKeyPair.address;
    gameState.transitWalletKeyPair = crypto.createKeyPair();
    gameState.poolWalletKeyPair = {"address": "a317162377777dea68f05c88c8ff52362842a6df", "privateKey": "21df7a111c3452a3da0b4e051d9b1b3b68dc234e4f8c91c3bf30cb8f118eb7fa", "publicKey": "02c5db6110bab7653b17b0ef1412a2dd543a7d71a8e5c881c4bae7beb8343fc5d7"}
    gameState.projectWalletKeyPair = {"address": "8830e66a239bbd6682fb154df8b5bfc0e88088d1", "privateKey": "ec336b113b86bf2e0dd9a1ee91309bbec6b1e671b6c2209d47a2a0abcf495f82", "publicKey": "03347a8b16ca874f41c59d238b37ca64fb3305fb3e451d1f077b593bf9bdf770fa"}
    gameState.profitWalletKeyPair = {"address": "db6403750d902a40df2b90f7d781412094e3dc73", "privateKey": "3f8dbd33b002b585fb872c8c877f39c7dd1a1e6b386c0edfde3dafb72f45d6a3", "publicKey": "03bc6741e26d3e8cc03aff3a2b46fa7bf44d4b7a023c134d7a0c17edf1cfd1a9d8"}

    await initCilUtils();
    updateBalance();
    setInterval(() => {
      updateBalance();
    }, 30000)
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
    gameState.parityList.length = 0;
  };

  const generalReset = async () => {
    gameState.balance = 1000000;   
    gameState.previousBalance = 0;
    gameState.bid = null;
    gameState.bidForBalanceChart = 0;
    gameState.round = 3;
    gameState.parityList.length = 0;
  };

  const refundFunds = async () => {
    console.log('REFUND')
    const txList = await gameState.gameWalletCilUtils.getTXList();
    for (let j = 0; j < txList.length; j++) {
        if (txList[j].outputs.length == 1 && txList[j].outputs[0].to == gameState.gameWalletKeyPair.address) {
            const balance = await gameState.gameWalletCilUtils.getBalance()
            console.log('Performing refund');
            console.log('Balance: ' + balance)
            console.log(txList[j]);
            console.log("Sending all " + balance + " UBX to: " + txList[j].inputs[0].from)
            const txFunds = await gameState.gameWalletCilUtils.createSendCoinsTx([
                [txList[j].inputs[0].from, -1]], 0);
            await gameState.gameWalletCilUtils.sendTx(txFunds);
            await gameState.gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
            console.log('Refunded all ' + balance + ' UBX to: ' + txList[j].inputs[0].from)
        }
    }
  }

  const startGame = async () => {
    if (gameState.parityList.length > 0) {
      resetGame();
    }
    gameState.previousBalance = gameState.balance;

    let currentBalance = gameState.bid || 0;
    const bid = Number(gameState.bid);
    gameState.bidForBalanceChart = bid;
    // Send bid to transit wallet
    const txFunds = await gameState.gameWalletCilUtils.createSendCoinsTx([
      [gameState.transitWalletKeyPair.address, bid]], 0);
    await gameState.gameWalletCilUtils.sendTx(txFunds);
    await gameState.gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

    console.log(gameState.round);
    const tParityList = [];
    for (let i = 0; i < gameState.round; i++) {
      console.log(i);
      const randomNumber = getRandomNumber();
      const hash = number2Hash(randomNumber);
      const parity = randomNumber % 2 !== 0;

      if (currentBalance <= 0) {
        break;
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
      await updateBalance();

      tParityList.push({
        round: i,
        number: randomNumber,
        parity: parity,
        currentBalance: currentBalance,
        hashNumber: hash,
      });
    }
    console.log(tParityList)
    gameState.parityList = tParityList;
    if (currentBalance >= (gameState.bid || 0)) {
      // Send all funds from transit wallet to pool wallet
      let txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
        [gameState.poolWalletKeyPair.address, -1]], 0);
      await gameState.transitWalletCilUtils.sendTx(txFunds);    
      await gameState.transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

      // Send CurrentBalance from pool wallet to game wallet
      txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
        [gameState.gameWalletKeyPair.address, currentBalance]], 0);
      await gameState.poolWalletCilUtils.sendTx(txFunds);    
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

      // Send 2% of CurrentBalance from pool wallet to project Wallet
      txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
        [gameState.projectWalletKeyPair.address, currentBalance * 0.02]], 0);
      await gameState.poolWalletCilUtils.sendTx(txFunds);
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    } else {
      const arrUtxos = await gameState.transitWalletCilUtils.getUtxos();
      const walletBalance = arrUtxos.reduce((accum: any, current: any) => accum + current.amount, 0);
      const txCost = gameState.transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
      const sumToSend = walletBalance - txCost;      

      // Send from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
      const txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
        [gameState.projectWalletKeyPair.address, sumToSend * 0.02],
        [gameState.profitWalletKeyPair.address, sumToSend * 0.784],
        [gameState.poolWalletKeyPair.address, sumToSend * 0.196]
      ], 0);
      await gameState.transitWalletCilUtils.sendTx(txFunds);
      await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
      await updateBalance();
    }
  };

  return {
    gameState,
    setBid,
    generalReset,
    restoreWallet,
    refundFunds,
    number2Hash,
    hash2Number,
    startGame,
    copyWallet,
    generateWallet,
  };
});
