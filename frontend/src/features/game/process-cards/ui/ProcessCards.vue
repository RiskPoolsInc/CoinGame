<script setup lang="ts">
import BalanceChart from "@/entities/game/ui/balance-chart";
import { useGameStore } from "@/entities/game/model/game";
import { PROCESS_CARDS_TABLE_COLUMNS } from "@/features/game/process-cards/model/constants";
import HashTable2 from "@/features/game/hash-table-2";
import { computed, ref, watch } from "vue";

const { gameState } = useGameStore();

let list = ref<any[]>([]);

const datasets = computed(() => gameState.parityList);

watch(
  datasets,
  async (newSet) => {
    console.log("newSet", newSet);
    let i = 0;
    const interval = setInterval(() => {
      if (i === newSet.length || !newSet.length) {
        clearInterval(interval);
        return;
      }

      add2List(newSet[i]);
      i++;
    }, 1000);
  },
  { deep: true }
);

const add2List = (value: any) => {
  list.value = [...list.value, value];
};
</script>

<template>
  <div class="process-card row justify-between">
    <div class="col-lg-6">
      <div class="process-card__title">Your game progress chart, balance.</div>
    </div>
    <div class="col-lg-5 xs-hide md-hide">
      <div class="process-card__table-title">
        The results of your game, randomly generated numbers. Odd numbers -win,
        even numbers - loss.
      </div>
    </div>
    <div class="col-lg-6 col-xs-12">
      <BalanceChart />
    </div>

    <div class="col-lg-5 col-md-12 xl-hide lg-hide">
      <div class="process-card__table-title">
        The results of your game, randomly generated numbers. Odd numbers -win,
        even numbers - loss.
      </div>
    </div>

    <div class="col-lg-6 col-md-12 col-xs-12">
      <div class="process-card__table">
        <HashTable2 :columns="PROCESS_CARDS_TABLE_COLUMNS" :rows="list" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
