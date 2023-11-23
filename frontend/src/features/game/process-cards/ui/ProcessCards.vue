<script setup lang="ts">
import BalanceChart from "@/entities/game/ui/balance-chart";
import HashTable from "@/features/game/hash-table/ui/HashTable.vue";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const columns = [
  {
    name: "Round",
    label: "Round",
    align: "left",
    field: "round",
  },
  {
    name: "result",
    align: "center",
    label: "Result",
    field: "number",
  },
  {
    name: "answer",
    align: "center",
    label: "PARITY",
    field: "parity",
  },
];
</script>

<template>
  <div class="process-card row justify-between">
    <div class="col-lg-6">
      <BalanceChart />
    </div>

    <div class="col-lg-5">
      <HashTable :columns="columns" :rows="gameState.parityList" />
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
