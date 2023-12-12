<script setup lang="ts">
import { computed } from "vue";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const resultGame = computed(() => {
  if (!gameState.previousBalance) {
    return 0;
  }
  return (
    gameState.parityList[gameState.parityList.length - 1].currentBalance || 0
  );
});
</script>

<template>
  <div class="result-game">
    <div class="row justify-lg-end justify-md-center justify-sm-center">
      <div class="col-lg-6 col-md-12 col-sm-7 col-xs-12 row result-game__item">
        <div class="result-game__label">Your game result, UBX</div>
        <VInput v-model="resultGame" disabled class="result-game__input" />
      </div>

      <div class="col-lg-6 col-md-12 col-sm-7 col-xs-12 row result-game__item">
        <div class="result-game__label">Your game wallet balance, UBX</div>
        <VInput
          v-model="gameState.balance"
          class="result-game__input"
          disabled
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
