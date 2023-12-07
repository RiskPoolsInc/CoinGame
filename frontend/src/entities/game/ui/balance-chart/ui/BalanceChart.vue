<script setup lang="ts">
import { computed, onMounted, reactive, ref, watch } from "vue";
import ApexCharts from "apexcharts";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

let list = ref<Array<number>>([]);

let chart = reactive<any>(null);

let annotations = reactive<any>([]);

const datasets = computed(() => {
  const list = gameState.parityList.map((item) => item.currentBalance);
  return list.length > 0 ? [gameState.bid, ...list] : [];
});

const points = computed<any[]>(() => {
  const list = gameState.parityList.map((item) =>
    makePoint(item.currentBalance, item.round)
  );
  return list;
});

watch(datasets, async (newSet) => {
  let i = 0;
  list.value = [];
  annotations = [];

  // add2List(newSet[0]);

  const interval = setInterval(() => {
    if (i === newSet.length || !newSet.length) {
      clearInterval(interval);
      return;
    }

    add2List(newSet[i]);
    if (points.value[i]) {
      add2Annotations(points.value[i]);
    }

    chart.updateSeries([
      {
        data: list.value,
      },
    ]);

    chart.updateOptions({
      annotations: {
        points: annotations,
      },
    });
    i++;
  }, 1000);
});

const makePoint = (value: number, round: number) => {
  return {
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
  };
};

const add2List = (value: number) => {
  list.value = [...list.value, value];
};

const add2Annotations = (annotation: any) => {
  annotations = [...annotations, annotation];
};

onMounted(() => {
  var options = {
    series: [
      {
        name: "Round",
        data: list.value,
      },
    ],
    labels: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
    annotations: {
      points: annotations,
    },
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
      toolbar: {
        show: false,
      },
      fontFamily: "Padauk",
      offsetY: -20,
    },
    tooltip: {
      enabled: false,
    },
    colors: ["#9196DB", "#9196DB"],
    dataLabels: {
      enabled: false,
    },
    stroke: {
      curve: "straight",
    },
    grid: {
      show: true,
      borderColor: "rgba(191, 201, 226, 0.50)",
      strokeDashArray: 0,
      position: "back",
      xaxis: {
        lines: {
          show: true,
        },
      },
      yaxis: {
        lines: {
          show: true,
        },
      },
    },
    markers: {
      size: 0,
    },
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
        formatter: function (val: string) {
          return parseInt(val);
        },
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
    legend: {
      display: false,
    },
  };

  chart = new ApexCharts(document.querySelector("#chart"), options);
  chart.render();
});
</script>

<template>
  <div id="acquisitions">
    <div id="chart"></div>
    <div class="title">ROUNDS</div>
  </div>
</template>

<style scoped lang="scss">
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
