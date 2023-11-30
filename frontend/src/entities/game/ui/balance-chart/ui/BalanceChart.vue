<script setup lang="ts">
import { computed, onMounted, ref, watch } from "vue";
import Chart from "chart.js/auto";
import { Line } from "vue-chartjs";
import { useGameStore } from "@/entities/game/model/game";

const { gameState } = useGameStore();

let list = ref<Array<number>>([]);

const datasets = computed(() => {
  const list = gameState.parityList.map((item) => item.currentBalance);
  return list.length > 0 ? [gameState.bid, ...list] : [];
});

watch(datasets, async (newSet) => {
  let i = 0;
  const interval = setInterval(() => {
    if (i === newSet.length || !newSet.length) {
      clearInterval(interval);
      return;
    }

    add2List(newSet[i]);
    i++;
  }, 1000);
});

const add2List = (value: number) => {
  list.value = [...list.value, value];
};

const chartData = computed(() => {
  return {
    labels: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
    datasets: [
      {
        data: list.value,
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

  pointRadius: 10,
  pointHoverRadius: 15,
  borderColor: "#9196DB",
  backgroundColor: "rgba(145, 150, 219, 0.50)",

  scales: {
    y: {
      ticks: {
        stepSize: 20000,
      },
    },
  },
};

onMounted(() => {
  const element = document.getElementById(
    "acquisitions1"
  ) as HTMLCanvasElement | null;

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
