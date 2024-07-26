<script setup lang="ts">
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import {useGame} from "@/shared/composables/useGame";

const {formattedBalance, formattedRewardSum} = useGame()
</script>

<template>
  <div class="result-game">
    <div class="row justify-lg-end justify-md-center justify-sm-center">
      <div class="col-lg-6 col-md-12 col-sm-7 col-xs-12 row result-game__item">
        <div class="result-game__label">Your game result, UBX</div>
        <VInput v-model="formattedRewardSum" disabled class="result-game__input" />
      </div>

      <div class="col-lg-6 col-md-12 col-sm-7 col-xs-12 row result-game__item">
        <div class="result-game__label">Your game wallet balance, UBX</div>
        <VInput
          :model-value="formattedBalance"
          class="result-game__input"
          disabled
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
