<script setup lang="ts">
import { computed, onMounted } from "vue";
import Chart from "chart.js/auto";

import { Line } from "vue-chartjs";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const labels = computed(() => {
  return gameState.parityList.map((item) => item.round);
});

const chartData = computed(() => {
  return {
    labels: labels.value,
    datasets: [{ data: [40, 20, 12] }],
  };
});

const chartOptions = {
  responsive: true,
};

onMounted(() => {
  const element = document.getElementById("acquisitions");

  // Need to refactor this
  new Chart(element, {
    type: "line",
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May"],
      datasets: [
        {
          label: "2018 Sales",
          data: [300, 700, 450, 750, 450],
        },
      ],
    },
  });
});
</script>

<template>
  <div id="acquisitions">
    <Line :data="chartData" :options="chartOptions" />
  </div>
</template>

<style scoped lang="scss"></style>
