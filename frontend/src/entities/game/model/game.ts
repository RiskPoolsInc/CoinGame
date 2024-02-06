// eslint-disable-next-line @typescript-eslint/no-var-requires
const crypto = require('crypto-web');

import {defineStore} from "pinia";
import {reactive} from "vue";
import {sha224, sha256} from 'js-sha256';

import {IGameState} from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    wallet: "",
    balance: 1000000,
    previousBalance: 0,
    bid: 0,
    round: 3,
    parityList: [],
  });

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const generateWallet = () => {
    const kp = crypto.createKeyPair();
    gameState.wallet = kp.privateKey;
    // gameState.wallet = new Date().getTime().toString();
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

  const generalReset = () => {
    gameState.balance = 1000000;
    gameState.previousBalance = 0;
    gameState.bid = 0;
    gameState.round = 3;
    gameState.parityList = [];
  };

  const startGame = () => {
    if (gameState.parityList.length > 0) {
      resetGame();
    }

    gameState.previousBalance = gameState.balance;

    let currentBalance = gameState.bid;

    for (let i = 0; i < gameState.round; i++) {
      const randomNumber = getRandomNumber();
      const hash = number2Hash(randomNumber);
      const parity = randomNumber % 2 !== 0;

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
