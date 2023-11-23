import { defineStore } from "pinia";
import { reactive } from "vue";
import { IGameState } from "@/entities/game/model/game.interface";

export const useGameStore = defineStore("game", () => {
  const gameState = reactive<IGameState>({
    wallet: "",
    balance: 1000000,
  });

  const generateWallet = () => {
    gameState.wallet = new Date().getTime().toString();
  };

  const copyWallet = () => {
    navigator.clipboard.writeText(gameState.wallet);
  };

  return {
    gameState,
    copyWallet,
    generateWallet,
  };
});
