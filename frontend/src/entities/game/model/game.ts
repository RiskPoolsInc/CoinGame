// eslint-disable-next-line
// @ts-ignore
// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');
// eslint-disable-next-line @typescript-eslint/no-var-requires
import axios from "@/shared/lib/plugins/axios";

import {defineStore} from "pinia";
import {computed, reactive} from "vue";
import {sha224, sha256} from 'js-sha256';
import {useCookies} from '@vueuse/integrations/useCookies'
import {copyToClipboard} from "quasar";
import {useIntervalFn, useLocalStorage} from '@vueuse/core';
import initCilInstance from "@/shared/lib/utils/initCilInstance";
import {IGameState, IParityList} from "@/entities/game/model/game.interface";

const cookies = useCookies()

export const useGameStore = defineStore("game", () => {
  const isPlaying = useLocalStorage<boolean>('isPlaying', false);
  const isRuning = useLocalStorage<boolean>('isRuning', false);
  const gameId = useLocalStorage<string>('gameId', '');
  const wallet = useLocalStorage<Wallet>('wallet', {} as Wallet);
  const parityList = useLocalStorage<IParityList[]>('parityList', []);
  const bid = useLocalStorage<number>('bid', Number(process.env.VUE_APP_MIN_BID));
  const rewardSum = useLocalStorage<string>('rewardSum','0');
  const balanceInterval = useIntervalFn(async () => {
    await updateBalance()
  }, 10000)
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
      hashNumber: round.hashForNumber,
      currentGameRoundSum: round.currentGameRoundSum,
      round: round.result.id,
      parity: round.result.code === 'Win',
      number: round.number
    }))
  }

  const getCurrentGame = async () => {
    const response = await axios.get('v1/games/current', {params: {WalletId: wallet.value.id}})
    if (!response.data && currentGameInterval.isActive) {
      currentGameInterval.pause()
      isRuning.value = false
      const latestGame = await getLatestGame()
      updateParityList(latestGame.gameRounds)
      rewardSum.value = `${latestGame.rewardSum}`
      await updateBalance()
      gameState.inProgress = false
      gameState.isPrepared = false
      isPlaying.value = false
      balanceInterval.resume()
    }
    if (response.data.gameRounds) {
      updateParityList(response.data.gameRounds)
    }
  }

  const getLatestGame = async () => {
    const response = await axios.get('v1/games', {params: {WalletId: wallet.value.id}})
    if (!response.data.length) return
    return response.data.find((game: { id: string }) => game.id === gameId.value)
  }

  const restoreGame = async (prepareCb: () => void) => {
    if (!gameId.value.length) return
    try {
      const latestGame = await getLatestGame()
      if (!latestGame) return
      balanceInterval.pause()
      gameState.round = latestGame.roundQuantity;
      if (latestGame.state.code === 'Created' && !latestGame.gameRounds.length && !isRuning.value) {
        prepareCb()
        gameState.inProgress = true
      }
      if (latestGame.state.code === 'Created' && !latestGame.gameRounds.length && isRuning.value) {
        gameState.inProgress = true
        currentGameInterval.resume()
        updateParityList(latestGame.gameRounds)
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
    gameId.value = ''
    isRuning.value = false
  };

  const runGame = async () => {
    try {
      currentGameInterval.resume()
      isRuning.value = true
      const response = await axios.put('v1/games/run', {
        WalletId: wallet.value.id
      })
      updateParityList(response.data.gameRounds)
      rewardSum.value = `${response.data.rewardSum}`
      await updateBalance()
      balanceInterval.resume()
    } catch (e) {
      console.error(e)
    } finally {
      currentGameInterval.pause()
      gameState.inProgress = false
      gameState.isPrepared = false
      isRuning.value = false
      isPlaying.value = false
    }
  }

  const startGame = async () => {
    balanceInterval.pause()
    gameState.inProgress = true;
    parityList.value = []
    isPlaying.value = true
    rewardSum.value = '0'
    try {
      const response = await axios.put('v1/games/new', {
        rounds: gameState.round,
        rate: bid.value,
        WalletId: wallet.value.id
      })
      gameId.value = response.data.game.id
      gameState.balance -= response.data.sum + response.data.fee
    } catch (e) {
      gameState.inProgress = false;
      gameState.isPrepared = false;
      isRuning.value = false
      isPlaying.value = false
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
  };
});
