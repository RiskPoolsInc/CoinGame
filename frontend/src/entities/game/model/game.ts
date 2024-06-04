// eslint-disable-next-line
// @ts-ignore
import { CIL_UTILS_API_URL, CIL_UTILS_RPC_PORT, CIL_UTILS_RPC_ADDRESS, CIL_UTILS_RPC_USER, CIL_UTILS_RPC_PASS, BACKEND_URL } from '../../../../config'
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
import { copyToClipboard } from "quasar";

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
    uid: "",
    gameWalletAddress: ""
  });

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const restoreWallet = async () => {
    if (cookies.get('uid')) {
      generateWallet(true);
    }
  }

  const updateBalance = async () => {
    let nBalance = 0;
    const res = await api_backend.get('get-balance' + '?uid=' + encodeURIComponent(gameState.uid), {
      headers: {
      }
    })
      .then((response) => {
        console.log(response.data);
        nBalance = response.data.balance
      })
      .catch((error) => {
        console.log(error)
      })
    gameState.balance = Number(nBalance);
  }

  const generateWallet = async (restoreWallet = false) => {
    if (restoreWallet) {
      gameState.uid = cookies.get('uid');
    } else {
      gameState.uid = (Math.random().toString(36) + '00000000000000000').slice(2, 16 + 2)
    }
    console.log(gameState.uid)
    cookies.set('uid', gameState.uid);
    const res = await api_backend.get('create-game-wallet' + '?uid=' + encodeURIComponent(gameState.uid), {
      headers: {
      }
    })
      .then((response) => {
        console.log(response.data);
        gameState.gameWalletAddress = response.data.address
      })
      .catch((error) => {
        console.log(error)
      })

    if (gameState.gameWalletAddress.length > 0) {
      gameState.wallet = 'Ux' + gameState.gameWalletAddress;
    }

    updateBalance();
    setInterval(() => {
      updateBalance();
    }, 10000)
  };

  const copyWallet = async () => {
    await copyToClipboard(gameState.wallet);
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
    const res = await api_backend.get('refund-funds' + '?uid=' + encodeURIComponent(gameState.uid), {
      headers: {
      }
    })
      .then((response) => {
        gameState.inProgress = false
        console.log(response.data);
      })
      .catch((error) => {
        gameState.inProgress = false
        console.log(error)
      })
      .finally(() => {
        gameState.inProgress = false
      })
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

    const res = await api_backend.get('play-game' + '?round=' + encodeURIComponent(gameState.round) +
      '&bid=' + encodeURIComponent(bid) +
      '&uid=' + encodeURIComponent(gameState.uid), {
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
            if (response.data.status !== "-1") {
              gameState.parityList = response.data.parityList;
            }
            if (response.data.status == "1") {
              gameState.inProgress = false;
              await updateBalance();
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
