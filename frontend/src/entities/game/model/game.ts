// eslint-disable-next-line
// @ts-ignore
// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');
// eslint-disable-next-line @typescript-eslint/no-var-requires
import axios from "@/shared/lib/plugins/axios";

import { defineStore } from "pinia";
import {computed, reactive} from "vue";
import { sha224, sha256 } from 'js-sha256';
import { useCookies } from '@vueuse/integrations/useCookies'
import { copyToClipboard } from "quasar";
import {useIntervalFn, useLocalStorage} from '@vueuse/core';
import initCilInstance from "@/shared/lib/utils/initCilInstance";

const cookies = useCookies()

import {IGameState, IParityList} from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const isRefunded = useLocalStorage<boolean>('isRefunded', true);
  const wallet = useLocalStorage<Wallet>('wallet', {} as Wallet);
  const parityList = useLocalStorage<IParityList[]>('parityList', []);
  const bid = useLocalStorage<number>('bid', Number(process.env.VUE_APP_MIN_BID));
  const rewardSum = useLocalStorage<string>('rewardSum','0');
  const balanceInterval = useIntervalFn(async () => {
    await updateBalance()
  }, 30000)
  const currentGameInterval = useIntervalFn(async () => {
    await getCurrentGame()
  }, 5000, {immediate: false})

  const gameState = reactive<IGameState>({
    inProgress: false,
    wallet: "",
    balance: 0,
    previousBalance: 0,
    round: 3,
    uid: "",
    gameWalletAddress: "",
    isPrepared: false
  });

  const hasWallet = computed(() => typeof wallet.value !== 'undefined' && !!Object.keys(wallet.value).length)

  const address = computed(() => gameState.wallet && `Ux${gameState.wallet}`)


  // Mutations
  const setBid = (data: number) => {
    bid.value = data;
  };

  const updateParityList = (gameRounds: ResponseGameRound[]) => {
    parityList.value = gameRounds.sort((a, b) => a.roundNumber - b.roundNumber).map((round) => ({
      hashNumber: round.id,
      currentGameRoundSum: round.currentGameRoundSum,
      round: round.result.id,
      parity: round.result.code === 'Win',
      number: round.number
    }))
  }

  const getCurrentGame = async () => {
    const response = await axios.get('v1/games/current', {params: {WalletId: wallet.value.id}})
    if (response.data.gameRounds)
      updateParityList(response.data.gameRounds)
  }

  const restoreGame = async (prepareCb: () => void) => {
    try {
      const response = await axios.get('v1/games', {params: {WalletId: wallet.value.id}})
      if (!response.data) return
      balanceInterval.pause()
      gameState.round = response.data.roundQuantity;
      if (response.data.state.code === 'Created' && !response.data.gameRounds.length) {
        prepareCb()
        gameState.inProgress = true
      }
    } catch (e) {
      console.error(e)
    }
  }

  const generateWallet = async () => {
    try {
      const res = await axios.put('v1/wallets/create')
      gameState.wallet = res.data.hash
      wallet.value = res.data
      await updateBalance()
      balanceInterval.resume()
    } catch (e) {
      console.error(e)
    }
  };

  const restoreWallet = async () => {
    gameState.wallet = wallet.value.hash
    await updateBalance()
  }

  const updateBalance = async () => {
    const instance = await initCilInstance()
    gameState.balance = await instance.getBalance(gameState.wallet)
  }

  const copyWallet = async () => {
      await copyToClipboard(address.value);
  };

  const number2Hash = (number: number) => {
    return sha256(String(number));
  };

  const hash2Number = (hash: string) => {
    return sha224(hash);
  };


  const generalReset = async () => {
    parityList.value = []
    bid.value = Number(process.env.VUE_APP_MIN_BID)
    wallet.value = undefined
    rewardSum.value = '0'
    gameState.balance = 0;
    gameState.wallet = '';
  };

  const runGame = async () => {
    try {
      currentGameInterval.resume()
      const response = await axios.put('v1/games/run', {
        WalletId: wallet.value.id
      })
      updateParityList(response.data.gameRounds)
      rewardSum.value = `${response.data.rewardSum}`
      await updateBalance()
    } catch (e) {
      console.error(e)
    } finally {
      currentGameInterval.pause()
      gameState.inProgress = false
      gameState.isPrepared = false
    }
  }

  const startGame = async () => {
    balanceInterval.pause()
    gameState.inProgress = true;
    parityList.value = []
    isRefunded.value = false
    try {
      const response = await axios.put('v1/games/new', {
        rounds: gameState.round,
        rate: bid.value,
        WalletId: wallet.value.id
      })
      gameState.balance -= response.data.sum + response.data.fee
    } catch (e) {
      gameState.inProgress = false;
      throw e
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
    restoreWallet,
    hasWallet,
    address,
    runGame,
    parityList,
    bid,
    wallet,
    rewardSum,
    restoreGame
//    getCurrentGame
  };
});
