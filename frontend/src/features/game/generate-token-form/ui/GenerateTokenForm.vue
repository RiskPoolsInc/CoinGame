<script setup lang="ts">
import { ref } from "vue";
import VButton from "@/shared/ui/base-components/v-button";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";

import { useGameStore } from "@/entities/game/model/game";

const { gameState, generateWallet, copyWallet } = useGameStore();

const copyingWallet = ref(false);

const handleCopyWallet = () => {
  copyingWallet.value = true;

  setTimeout(() => {
    copyingWallet.value = false;
  }, 1000);
  copyWallet();
};
</script>

<template>
  <div class="generate-token-form">
    <div class="generate-token__form row justify-between items-stretch">
      <VButton
        label="GENERATE"
        color="white"
        text-color="dark"
        class="mt-auto"
        @click="generateWallet"
      />

      <VInput
        v-model="gameState.wallet"
        class="col-lg-4"
        label="Your game wallet"
      />

      <VInput
        v-model="gameState.balance"
        disabled
        class="col-lg-3"
        label="Wallet balance, UBX"
      />

      <VButton
        v-if="gameState.wallet"
        label="COMPLETED"
        color="white"
        text-color="dark"
        class="mt-auto"
      />

      <VButton
        v-else
        label="EMPTY"
        color="red-14"
        text-color="dark"
        class="mt-auto"
      />

      <VButton
        :label="copyingWallet ? 'COPIED' : 'COPY WALLET'"
        :disabled="!gameState.wallet"
        color="white"
        text-color="dark"
        class="mt-auto"
        @click="handleCopyWallet"
      />
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
