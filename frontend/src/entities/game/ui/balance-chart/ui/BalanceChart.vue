<script setup lang="ts">
import Chart from "chart.js/auto";
import { onMounted } from "vue";

onMounted(() => {
  new Chart(document.getElementById("acquisitions"), {
    type: "bar",
    options: {
      animation: false,
      plugins: {
        legend: {
          display: false,
        },
        tooltip: {
          enabled: false,
        },
      },
    },
    data: {
      labels: [],
      datasets: [
        {
          label: "Acquisitions by year",
          data: [],
        },
      ],
    },
  });
});
</script>

<template>
  <div id="acquisitions">asdsad</div>
</template>

<style scoped lang="scss"></style>
