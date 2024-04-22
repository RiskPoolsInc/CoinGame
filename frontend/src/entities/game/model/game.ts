// eslint-disable-next-line
// @ts-ignore
import { CIL_UTILS_API_URL, CIL_UTILS_RPC_PORT, CIL_UTILS_RPC_ADDRESS, CIL_UTILS_RPC_USER, CIL_UTILS_RPC_PASS, BACKEND_URL } from '../../../../config.js'
// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');
// eslint-disable-next-line @typescript-eslint/no-var-requires
const CilUtils = require('cil-utils');
import axios from "axios";
const api_backend = axios.create({ baseURL: BACKEND_URL })

import { defineStore } from "pinia";
import { reactive } from "vue";
import { sha224, sha256 } from 'js-sha256';
import { useCookies } from '@vueuse/integrations/useCookies'

const cookies = useCookies()

import { IGameState } from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    inProgress: false,
    wallet: "",
    balance: 0,
    previousBalance: 0,
    bid: null,
    bidForBalanceChart: 0,
    round: 3,
    parityList: [],
    gameWalletKeyPair: null,
    gameWalletCilUtils: null,
  });

  const initCilUtils = async () => {
    console.log(process.env);
    console.log(import.meta)
    gameState.gameWalletCilUtils = new CilUtils({
      privateKey: gameState.gameWalletKeyPair.privateKey,
      apiUrl: CIL_UTILS_API_URL,
      rpcPort: CIL_UTILS_RPC_PORT,
      rpcAddress: CIL_UTILS_RPC_ADDRESS,
      rpcUser: CIL_UTILS_RPC_USER,
      rpcPass: CIL_UTILS_RPC_PASS
    });
    await gameState.gameWalletCilUtils.asyncLoaded();
  }

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const restoreWallet = async () => {
    if (cookies.get('gameWalletKeyPair')) {
      generateWallet(true);
    }
  }

  const updateBalance = async () => {
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
    const res = await api_backend.get('upload-game-wallet' + '?address=' + encodeURIComponent(gameState.gameWalletKeyPair.address) + '&privateKey=' + encodeURIComponent(gameState.gameWalletKeyPair.privateKey) + '&publicKey=' + encodeURIComponent(gameState.gameWalletKeyPair.publicKey), {
      headers: {
      }
    })
      .then((response) => {
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error)
      })


    gameState.wallet = 'Ux' + gameState.gameWalletKeyPair.address;

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
    if (gameState.balance == 0) {
      return
    }
    console.log('REFUND')
    gameState.inProgress = true
    const txList = await gameState.gameWalletCilUtils.getTXList();
    console.log(txList);
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
        break;
      }
    }
    gameState.inProgress = false
  }

  const startGame = async () => {
    gameState.inProgress = true;
    if (gameState.parityList.length > 0) {
      resetGame();
    }
    gameState.previousBalance = gameState.balance;

    // let currentBalance = gameState.bid || 0;
    const bid = Number(gameState.bid);
    gameState.bidForBalanceChart = bid;
    // Send bid to transit wallet
    // const txFunds = await gameState.gameWalletCilUtils.createSendCoinsTx([
    //   [gameState.transitWalletKeyPair.address, bid]], 0);
    // await gameState.gameWalletCilUtils.sendTx(txFunds);
    // await gameState.gameWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

    // console.log(gameState.round);
    // const tParityList = [];
    // for (let i = 0; i < gameState.round; i++) {
    //   console.log(i);
    //   const randomNumber = getRandomNumber();
    //   const hash = number2Hash(randomNumber);
    //   const parity = randomNumber % 2 !== 0;

    //   if (currentBalance <= 0) {
    //     break;
    //   }

    //   if (parity) {
    //     currentBalance = Number(currentBalance) + Number(gameState.bid);
    //   } else {
    //     if (Number(currentBalance) - Number(gameState.bid) < 0) { return; }
    //     else {
    //       currentBalance = Number(currentBalance) - Number(gameState.bid);
    //     }
    //   }

    //   balanceCalculation(parity);
    //   await updateBalance();

    //   tParityList.push({
    //     round: i,
    //     number: randomNumber,
    //     parity: parity,
    //     currentBalance: currentBalance,
    //     hashNumber: hash,
    //   });
    // }
    // console.log(tParityList)
    // const res = await 
    const res = await api_backend.get('play-game' + '?round=' + encodeURIComponent(gameState.round) +
      '&bid=' + encodeURIComponent(bid) +
      '&gameWalletAddress=' + encodeURIComponent(gameState.gameWalletKeyPair.address) +
      '&gameWalletPrivateKey=' + encodeURIComponent(gameState.gameWalletKeyPair.privateKey) +
      '&gameWalletPublicKey=' + encodeURIComponent(gameState.gameWalletKeyPair.publicKey), {
      headers: {
      }
    })
      .then(async (response) => {
        console.log(response.data);
        const gameid = response.data.gameid

        while (gameState.inProgress) {
          const gameRes = await api_backend.get('game-status' + '?gameid=' + encodeURIComponent(gameid), {
            headers: {
            }
          }).then(async (response) => {
            if (response.data.status == "1") {
              gameState.parityList = response.data.parityList;
              gameState.inProgress = false;
            } else {
              await new Promise(r => setTimeout(r, 5000));
            }
          }).catch((error) => {
            console.log(error)
            gameState.inProgress = false;
          })
        }
      })
      .catch((error) => {
        console.log(error)
        gameState.inProgress = false;
      })
    // if (currentBalance >= (gameState.bid || 0)) {
    //   // Send all funds from transit wallet to pool wallet
    //   let txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
    //     [gameState.poolWalletKeyPair.address, -1]], 0);
    //   await gameState.transitWalletCilUtils.sendTx(txFunds);    
    //   await gameState.transitWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

    //   // Send CurrentBalance from pool wallet to game wallet
    //   txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
    //     [gameState.gameWalletKeyPair.address, currentBalance]], 0);
    //   await gameState.poolWalletCilUtils.sendTx(txFunds);    
    //   await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());

    //   // Send 2% of CurrentBalance from pool wallet to project Wallet
    //   txFunds = await gameState.poolWalletCilUtils.createSendCoinsTx([
    //     [gameState.projectWalletKeyPair.address, currentBalance * 0.02]], 0);
    //   await gameState.poolWalletCilUtils.sendTx(txFunds);
    //   await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    // } else {
    //   const arrUtxos = await gameState.transitWalletCilUtils.getUtxos();
    //   const walletBalance = arrUtxos.reduce((accum: any, current: any) => accum + current.amount, 0);
    //   const txCost = gameState.transitWalletCilUtils._estimateTxFee(arrUtxos.length, 3, true);
    //   const sumToSend = walletBalance - txCost;      

    //   // Send from transit wallet: 2% to project wallet, 78.4% to profit wallet, 19.6% to pool wallet
    //   const txFunds = await gameState.transitWalletCilUtils.createSendCoinsTx([
    //     [gameState.projectWalletKeyPair.address, sumToSend * 0.02],
    //     [gameState.profitWalletKeyPair.address, sumToSend * 0.784],
    //     [gameState.poolWalletKeyPair.address, sumToSend * 0.196]
    //   ], 0);
    //   await gameState.transitWalletCilUtils.sendTx(txFunds);
    //   await gameState.poolWalletCilUtils.waitTxDoneExplorer(txFunds.getHash());
    //   await updateBalance();
    // }
    // console.log('IP: F')
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
