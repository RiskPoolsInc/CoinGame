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
    // options: {
    //   plugins: {
    //     legend: {
    //       display: false,
    //     },
    //   },
    //   legend: {
    //     display: false,
    //   },
    // },
    options: {
      legend: {
        display: false,
      },
      tooltips: {
        enabled: false,
      },
    },
  };
});

const chartOptions = {
  responsive: true,
  labels: {
    color: "red",
  },
};

onMounted(() => {
  const element = document.getElementById(
    "acquisitions1"
  ) as HTMLCanvasElement | null;

  console.log(element);
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
