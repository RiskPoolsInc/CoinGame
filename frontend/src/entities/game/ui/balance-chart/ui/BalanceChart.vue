<script setup lang="ts">
import { onMounted } from "vue";
import Chart from "chart.js/auto";

import { Line } from "vue-chartjs";

const chartData = {
  labels: ["January", "February", "March"],
  datasets: [{ data: [40, 20, 12] }],
};
const chartOptions = {
  responsive: true,
};

onMounted(() => {
  new Chart(document.getElementById("acquisitions"), {
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
