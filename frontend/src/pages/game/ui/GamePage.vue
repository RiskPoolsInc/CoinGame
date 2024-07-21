<script setup lang="ts">
import { onMounted } from 'vue'
import { GenerateToken, ResultGame, TossACoin } from "@/entities/game";
import { useGameStore } from "@/entities/game/model/game";
import RefundBlock from "@/features/game/refund-block";
import ProcessCards from "@/features/game/process-cards/ui/ProcessCards.vue";
import VProgress from "@/shared/ui/base-components/v-progress/ui/VProgress.vue";

const { gameState, restoreWallet } = useGameStore();

onMounted(() => {
  restoreWallet()
})

</script>

<template>
  <div class="game-page">
    <div class="container">
      <div class="game-page__title">112x_Coin Game player</div>

      <GenerateToken />

      <TossACoin />

      <ProcessCards />

      <ResultGame />

      <RefundBlock />

    </div>
    <VProgress v-if="gameState.inProgress" class="fixed-bottom" label="DO NOT refresh the page and wait for the game results."/>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
