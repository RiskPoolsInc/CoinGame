<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import { defineEmits, defineProps } from "vue/dist/vue";
import { computed } from "vue";

interface IGenerateToken {
  wallet: string;
  walletBalance: string;
}
interface IGenerateTokenProps {
  data: IGenerateToken;
}

const props = defineProps<IGenerateTokenProps>();

const emit = defineEmits<{
  "update:modelValue": [IGenerateToken];
}>();

const getValue = computed({
  get: () => props.data,
  set: (val) => emit("update:modelValue", val),
});
</script>

<template>
  <div class="generate-token">
    <div class="container">
      <div class="generate-token__title">112PxA_Top up the wallet</div>

      <div class="generate-token__info">
        To participate in the game you need to top up your game wallet. Generate
        a wallet and top it up.
      </div>

      <div v-if="getValue" class="generate-token__form row justify-between">
        <VButton label="GENERATE" color="white" text-color="dark" />

        <VInput
          v-model="getValue.wallet"
          class="col-lg-4"
          label="Your game wallet"
        />

        <VInput
          v-model="getValue.walletBalance"
          class="col-lg-3"
          label="Wallet balance, UBX"
        />

        <VButton label="COMPLETED" color="white" text-color="dark" />

        <VButton label="COPY WALLET" color="white" text-color="dark" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
