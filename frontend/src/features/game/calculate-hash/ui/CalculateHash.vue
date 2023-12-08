<script setup lang="ts">
import { ref } from "vue";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";

const { number2Hash } = useGameStore();

const hash = ref("");

const number = ref();

const calculateHash = () => {
  hash.value = number2Hash(Number(number.value));
};
</script>

<template>
  <div class="calculate-hash row justify-center">
    <VInput
      v-model="number"
      class="calculate-hash__number"
      label="Enter a number"
    />

    <VButton
      class="mt-auto"
      label="CALCULATE HASH"
      color="white"
      text-color="dark"
      size="lg"
      @click="calculateHash"
    />

    <VInput
      class="calculate-hash__hash mt-auto"
      :model-value="hash"
      label="Hash value"
      disabled
    />
  </div>
</template>

<style lang="scss">
.calculate-hash__hash {
  .q-field__native {
    font-size: 16px;
  }
}
@media (max-width: 550px) {
  .calculate-hash__hash {
    .q-field__native {
      font-size: 10px;
    }
  }
}
</style>

<style scoped lang="scss">
@import "styles.module";
</style>
