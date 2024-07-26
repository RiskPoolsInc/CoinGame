<script setup lang="ts">
  import { computed } from "vue";
  import { useGameStore } from "@/entities/game/model/game";

  const gameStore = useGameStore();
  const data = computed(() => ([
      {x: 0, y: Number(gameStore.bid)},
      ...gameStore.parityList.map((round, x) => ({
        x: x + 1,
        y: round.currentGameRoundSum
      }))
  ]))

  const series = computed(() => ([{
    name: 'series-1',
    data: data.value
  }]))

  const labelStyle = computed(() => ({
    colors: "#BFC9E2",
    fontSize: "16px",
    fontFamily: "Padauk",
    fontWeight: 700,
  }))

  const options = computed(() => ({
    chart: {
      toolbar: {
        show: false
      }
    },
    tooltip: { enabled: false },
    colors: ["#9196DB", "#9196DB"],
    stroke: { curve: "straight" },
    markers: {
      size: 5,
      colors: ['#9196DB'],
      strokeColors: '#9196DB',
      strokeWidth: 2,
    },
    dataLabels: {
      enabled: true,
      offsetY: -10,
      formatter:  (val: number, opts: {dataPointIndex: number}) => {
        return opts.dataPointIndex === 0 ? '' : val;
      },
      style: {
        colors: ['#BFC9E2'],
        fontSize: "16px",
        fontFamily: "Padauk",
        fontWeight: 700,
      },
      background: {
        enabled: false,
      }
    },
    responsive: [
      {
        breakpoint: 480,
        options: {
          chart: {
            height: 300
          },
          dataLabels: {
            style: {
              colors: ['#000000'],
              fontSize: '8px'
            },
            background: {
              enabled: true,
            },
            offsetY: -5
          }
        }
      }
    ],
    grid: {
      show: true,
      borderColor: "rgba(191, 201, 226, 0.50)",
      strokeDashArray: 0,
      position: "back",
      xaxis: { lines: { show: true } },
      yaxis: { lines: { show: true } },
      padding: {
        right: 20
      }
    },
    xaxis: {
      min: 0,
      max: 10,
      tickAmount: 10,
      labels: {
        style: labelStyle.value,
        formatter:  (val: number) => {
          return val.toFixed(0);
        }
      }
    },
    yaxis: {
      show: true,
      showForNullSeries: false,
      labels: {
        style: labelStyle.value
      },
      showAlways: false,
    },
  }))
</script>

<template>
  <div id="acquisitions">
    <apexchart type="line" v-bind="{options, series}"/>
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
