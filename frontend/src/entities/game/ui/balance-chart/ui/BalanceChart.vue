<script setup lang="ts">
import { computed, onMounted } from "vue";
import Chart from "chart.js/auto";

import { Line } from "vue-chartjs";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const labels = computed(() => {
  return gameState.parityList.map((item) => item.round);
});

const datasets = computed(() => {
  return gameState.parityList.map((item) => item.currentBalance);
});

const chartData = computed(() => {
  return {
    labels: labels.value,
    datasets: [{ label: "2018 Sales", data: datasets.value }],
  };
});

const chartOptions = {
  responsive: true,
};

onMounted(() => {
  const element = document.getElementById(
    "acquisitions"
  ) as HTMLCanvasElement | null;

  if (element) {
    new Chart(element, {
      type: "line",
      data: {
        labels: [],
        datasets: [
          {
            label: "2018 Sales",
            data: [300, 700, 450, 750, 450],
          },
        ],
      },
    });
  }
  // Need to refactor this
});
</script>

<template>
  <div id="acquisitions">
    <Line :data="chartData" :options="chartOptions" />
  </div>
</template>

<style scoped lang="scss"></style>
