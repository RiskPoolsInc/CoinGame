<script setup lang="ts">
import { GenerateToken } from "@/entities/game";
import TossACoin from "@/entities/game/ui/toss-a-coin/ui/TossACoin.vue";
import RefundBlock from "@/features/game/refund-block";
</script>

<template>
  <div class="game-page">
    <div class="container">
      <GenerateToken />

      <TossACoin />

      <RefundBlock />
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
