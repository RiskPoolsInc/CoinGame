<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";

import { useGameStore } from "@/entities/game/model/game";
import { useQuasar } from "quasar";

const $q = useQuasar();

const { gameState, generateWallet, copyWallet } = useGameStore();

const handleCopyWallet = () => {
  copyWallet();

  $q.notify({
    message: "Wallet copied",
    color: "green-14",
    position: "top-right",
    icon: "check",
  });
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

      <div class="col-lg-3 row justify-between mt-auto">
        <VButton
          v-if="gameState.wallet"
          label="COMPLETED"
          color="white"
          text-color="dark"
        />

        <VButton
          v-else
          label="EMPTY"
          color="red-14"
          text-color="dark"
          class="mt-auto"
        />
        <VButton
          label="COPY WALLET"
          :disabled="!gameState.wallet"
          color="white"
          text-color="dark"
          @click="handleCopyWallet"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
