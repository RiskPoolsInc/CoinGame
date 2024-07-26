<script setup lang="ts">
import {computed, onMounted, watch} from 'vue'
import { GenerateToken, ResultGame, TossACoin } from "@/entities/game";
import { useGameStore } from "@/entities/game/model/game";
import RefundBlock from "@/features/game/refund-block";
import ProcessCards from "@/features/game/process-cards/ui/ProcessCards.vue";
import VProgress from "@/shared/ui/base-components/v-progress/ui/VProgress.vue";
import {useIntervalFn} from "@vueuse/core";
import {storeToRefs} from "pinia";
import axios from "@/shared/lib/plugins/axios";
import {useQuasar} from "quasar";

const gameStore = useGameStore();
const $q = useQuasar()
const {gameState} = storeToRefs(gameStore);
const preparedInterval = useIntervalFn(async () => {
  await isPrepared()
}, 30000, {immediate: false})

const processMessage = computed(() => {
  if (gameState.value.isPrepared && gameStore.gameState.inProgress) {
    return 'DO NOT refresh the page and wait for the game results.'
  } else if (gameStore.gameState.inProgress) {
    return 'We are processing your bet. Wait. DO NOT refresh the page'
  }
  return 'Done'
})

const isPrepared = async () => {
  try {
    const response = await axios.get(`v1/wallets/${gameStore.wallet.id}/games/prepared`)
    gameStore.gameState.isPrepared = response.data.isPreparedToRun
  } catch (e) {
    console.error(e)
    preparedInterval.pause()
    gameState.value.inProgress = false
    $q.notify({
      message: "Something went wrong",
      color: "negative",
      position: "top-right",
    });
  }
}

watch(() => gameState.value.isPrepared, async (newValue) => {
  if (newValue) {
    preparedInterval.pause()
    await gameStore.runGame()
  }
})

onMounted(async () => {
  await gameStore.restoreWallet()
})

</script>

<template>
  <div class="game-page">
    <div class="container">
      <div class="game-page__title">112x_Coin Game player</div>

      <GenerateToken />

      <TossACoin @start="preparedInterval.resume"/>

      <ProcessCards />

      <ResultGame />

      <RefundBlock />

    </div>
    <VProgress v-if="gameStore.gameState.inProgress" class="fixed-bottom" :label="processMessage"/>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
