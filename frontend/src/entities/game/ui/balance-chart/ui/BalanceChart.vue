<script setup lang="ts">
import { computed, onMounted } from "vue";
import Chart from "chart.js/auto";
import { Line } from "vue-chartjs";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const datasets = computed(() => {
  return gameState.parityList.map((item) => item.currentBalance);
});

const chartData = computed(() => {
  return {
    labels: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
    datasets: [
      {
        data: [gameState.previousBalance, ...datasets.value],
      },
    ],
  };
});

const chartOptions = {
  responsive: true,
  labels: {
    color: "red",
  },
  plugins: {
    legend: {
      display: false,
    },
  },
  // scales: {
  //   y: {
  //     ticks: {
  //       stepSize: 100000,
  //     },
  //   },
  // },
};

onMounted(() => {
  const element = document.getElementById(
    "acquisitions1"
  ) as HTMLCanvasElement | null;

  console.log(element);
  if (element) {
    new Chart(element, {
      type: "bar",
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
    <div class="title">ROUNDS</div>
  </div>
</template>

<style scoped lang="scss">
.title {
  margin: 10px auto;
  color: var(--color-white);
  font-size: 22px;
  font-weight: 700;
  text-align: center;
  font-family: Padauk;
}
@media (max-width: 550px) {
  .title {
    font-size: 14px;
  }
}
</style>
