import { defineStore } from "pinia";
import { reactive } from "vue";

import { IGameState } from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    wallet: "",
    balance: 1000000,
    bid: 0,
    round: 3,
    parityList: [],
  });

  // Mutations
  const setBid = (bid: number) => {
    gameState.bid = bid;
  };

  const generateWallet = () => {
    gameState.wallet = new Date().getTime().toString();
  };

  const copyWallet = () => {
    navigator.clipboard.writeText(gameState.wallet);
  };

  const number2Hash = (number: number) => {
    return number.toString(16);
  };

  const hash2Number = (hash: string) => {
    return parseInt(hash, 16);
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

  const startGame = () => {
    for (let i = 0; i < gameState.round; i++) {
      const randomNumber = getRandomNumber();
      const hash = number2Hash(randomNumber);
      const parity = randomNumber % 2 === 0;

      balanceCalculation(parity);

      gameState.parityList.push({
        round: i,
        number: randomNumber,
        parity: parity,
        currentBalance: gameState.balance,
        hashNumber: hash,
      });
    }
  };

  return {
    gameState,
    setBid,
    number2Hash,
    hash2Number,
    startGame,
    copyWallet,
    generateWallet,
  };
});
