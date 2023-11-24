<script setup lang="ts">
import { GenerateToken, ResultGame, TossACoin } from "@/entities/game";
import RefundBlock from "@/features/game/refund-block";
import ProcessCards from "@/features/game/process-cards/ui/ProcessCards.vue";
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
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
