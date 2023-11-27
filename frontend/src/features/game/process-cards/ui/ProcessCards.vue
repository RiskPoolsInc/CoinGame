<script setup lang="ts">
import BalanceChart from "@/entities/game/ui/balance-chart";
import { useGameStore } from "@/entities/game/model/game";
import { PROCESS_CARDS_TABLE_COLUMNS } from "@/features/game/process-cards/model/constants";
import HashTable2 from "@/features/game/hash-table-2";

const { gameState } = useGameStore();
</script>

<template>
  <div class="process-card row justify-between">
    <div class="col-lg-6">
      <div class="process-card__title">Your game progress chart, balance.</div>
    </div>
    <div class="col-lg-5">
      <div class="process-card__title">
        The results of your game, randomly generated numbers. Odd numbers -win,
        even numbers - loss.
      </div>
    </div>
    <div class="col-lg-6">
      <BalanceChart />
    </div>

    <div class="col-lg-6">
      <HashTable2
        :columns="PROCESS_CARDS_TABLE_COLUMNS"
        :rows="gameState.parityList"
      />
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
