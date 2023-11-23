<script setup lang="ts">
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import { ref } from "vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";

const aa = ref("");
</script>

<template>
  <div class="bid-card">
    <div class="row justify-center">
      <div class="col-lg-3">
        <VInput v-model="aa" label="Your bid, UBX" />
      </div>
    </div>

    <div class="bid-card__slider">
      <div class="bid-card__label">Number of rounds (3-10)</div>

      <q-slider v-model="aa" color="white" marker-labels :min="0" :max="10" />
    </div>

    <div class="bid-card__action row justify-center">
      <div class="col-lg-2">
        <VButton
          className="full-width"
          label="BID"
          color="white"
          text-color="dark"
        />
      </div>

      <div class="col-lg-2">
        <VButton
          className="full-width"
          label="TOSS A COIN"
          color="white"
          text-color="dark"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
