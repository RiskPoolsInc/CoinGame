<script setup lang="ts">
import { computed, onMounted, reactive, ref, watch } from "vue";
import ApexCharts from "apexcharts";
import { useGameStore } from "@/entities/game/model/game";

// Extract game state from the store
const { gameState } = useGameStore();

// Reactive and ref variables
const list = ref<number[]>([]);
const chart = ref<ApexCharts | null>(null);
const interval = ref<number | null>(null);
const annotations = ref<any[]>([]);

// Compute the datasets for the chart
const datasets = computed(() => {
  console.log('Compute datasets');
  const parityList = gameState.parityList.map(item => item.currentBalance);
  console.log(parityList);
  return parityList.length > 0 ? [gameState.bidForBalanceChart, ...parityList] : [];
});

// Compute the annotation points for the chart
const points = computed(() => {
  console.log('Compute points');
  return gameState.parityList.map(item => makePoint(item.currentBalance, item.round));
});

// Watch for changes in datasets and update the chart accordingly
watch(datasets, (newSet) => {
  if (interval.value) {
    clearInterval(interval.value);
  }

  let i = list.value.length; // Start from the current length of the list
  interval.value = setInterval(() => {
    if (i === newSet.length || !newSet.length) {
      clearInterval(interval.value!);
      return;
    }

    add2List(newSet[i]);
    if (points.value[i]) {
      add2Annotations(points.value[i]);
    }

    updateChart();

    i++;
  }, 1000);
});

// Function to create an annotation point
const makePoint = (value: number, round: number) => ({
  x: round + 1,
  y: value,
  marker: {
    size: 8.5,
    fillColor: "rgba(145, 150, 219, 0.50)",
    strokeColor: "#9196DB",
  },
  label: {
    offsetY: 0,
    borderColor: "transparent",
    style: {
      color: "#BFC9E2",
      background: "transparent",
      fontFamily: ["Padauk"],
      fontSize: "12px",
      fontWeight: 700,
    },
    text: value,
  },
});

// Function to add a value to the list
const add2List = (value: number) => {
  list.value = [...list.value, value];
};

// Function to add an annotation
const add2Annotations = (annotation: any) => {
  annotations.value = [...annotations.value, annotation];
};

// Function to update the chart with new data
const updateChart = () => {
  chart.value?.updateSeries([{ data: list.value }]);
  chart.value?.updateOptions({
    annotations: { points: annotations.value },
  });
};

// Initialize the chart on mount
onMounted(() => {
  const options = {
    series: [{ name: "Round", data: list.value }],
    labels: Array.from({ length: 11 }, (_, i) => i),
    annotations: { points: annotations.value },
    chart: {
      background: "#18212E",
      height: 350,
      type: "line",
      dropShadow: {
        enabled: true,
        color: "blue",
        top: 18,
        left: 7,
        blur: 10,
        opacity: 0.2,
      },
      toolbar: { show: false, tools: { zoom: false } },
      zoom: { enabled: false },
      fontFamily: "Padauk",
      offsetY: -20,
    },
    tooltip: { enabled: false },
    colors: ["#9196DB", "#9196DB"],
    dataLabels: { enabled: false },
    stroke: { curve: "straight" },
    grid: {
      show: true,
      borderColor: "rgba(191, 201, 226, 0.50)",
      strokeDashArray: 0,
      position: "back",
      xaxis: { lines: { show: true } },
      yaxis: { lines: { show: true } },
    },
    markers: { size: 0 },
    xaxis: {
      type: "numeric",
      tickAmount: 10,
      max: 10,
      labels: {
        style: {
          colors: "#BFC9E2",
          fontSize: "16px",
          fontFamily: "Padauk",
          fontWeight: 700,
        },
        formatter: (val: string) => parseInt(val),
      },
    },
    yaxis: {
      show: true,
      showForNullSeries: false,
      labels: {
        style: {
          colors: "#BFC9E2",
          fontSize: "16px",
          fontFamily: "Padauk",
          fontWeight: 700,
        },
      },
      showAlways: false,
    },
    legend: { display: false },
  };

  chart.value = new ApexCharts(document.querySelector("#chart") as HTMLElement, options);
  chart.value.render();
});
</script>

<template>
  <div id="acquisitions">
    <div id="chart"></div>
    <div class="title">ROUNDS</div>
  </div>
</template>

<style scoped lang="scss">
#acquisitions {
  margin-left: -10px;
  margin-top: 20px;
}

.title {
  margin: -20px auto 10px auto;
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
