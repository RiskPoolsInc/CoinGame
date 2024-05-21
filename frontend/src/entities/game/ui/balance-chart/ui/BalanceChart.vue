<script setup lang="ts">
import { computed, onMounted, ref, watch } from "vue";
import ApexCharts from "apexcharts";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

const list = ref<number[]>([]);
const chart = ref<ApexCharts | null>(null);
const interval = ref<number | null>(null);
const datasets = computed(() => {
  const parityList = gameState.parityList.map(item => item.currentBalance);
  return parityList.length > 0 ? [gameState.bidForBalanceChart, ...parityList] : [];
});

///const annotations = computed(() => points.value.map(item => item.y))

const points = computed(() => {
  return gameState.parityList.map(item => makePoint(item.currentBalance, item.round));
});

watch(() => gameState.inProgress, (newValue) => {
  if (newValue) {
    list.value = []
    updateChart()
  }
})

watch(datasets, (newSet) => {
  if (interval.value) {
    clearInterval(interval.value);
  }

  let i = list.value.length;
  interval.value = setInterval(() => {
    if (i === newSet.length || !newSet.length) {
      clearInterval(interval.value!);
      return;
    }

    add2List(newSet[i]);

    updateChart();

    i++;
  }, 1000);
});

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
      fontFamily: "Padauk",
      fontSize: "12px",
      fontWeight: 700,
    },
    text: value.toString(),
  },
});

const add2List = (value: number) => {
  list.value = [...list.value, value];
};

const updateChart = () => {
  chart.value?.updateSeries([{ data: list.value }]);
  chart.value?.updateOptions({
    annotations: { points: points.value },
  });
};

onMounted(() => {
  const options = {
    series: [{ name: "Round", data: list.value }],
    labels: Array.from({ length: 11 }, (_, i) => i),
    annotations: { points: points.value },
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
