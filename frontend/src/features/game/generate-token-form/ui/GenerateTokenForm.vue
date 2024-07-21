<script setup lang="ts">
import { useQuasar } from "quasar";
import { useGameStore } from "@/entities/game/model/game";

import VButton from "@/shared/ui/base-components/v-button";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";

import VChip from "@/shared/ui/base-components/v-chip";
import {useGame} from "@/shared/composables/useGame";

const $q = useQuasar();

const { gameState, generateWallet, copyWallet, hasWallet } = useGameStore();
const { formattedBalance } = useGame();

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
      <div
        class="generate-token-form__wrapper"
      >
      <div class="generate-token-form__items">
        <div class="mt-auto generate-token-form__generate">
          <VButton
            label="GENERATE"
            color="white"
            class-name="full-width"
            text-color="dark"
            size="lg"
            @click="generateWallet"
            :disabled="hasWallet"
          />
        </div>

        <div class="generate-token-form__wallet">
          <VInput
            :model-value="`Ux${gameState.wallet}`"
            disabled
            label="Your game wallet"
          />
        </div>

        <div class="generate-token-form__balance">
          <VInput
            :model-value="formattedBalance"
            disabled
            label="Wallet balance, UBX"
          />
        </div>
        <div class="mt-auto generate-token-form__chip">
          <VChip v-if="gameState.balance" color="success"> COMPLETED </VChip>
          <VChip v-else color="danger"> EMPTY </VChip>
        </div>
      </div>

        <div
          class="generate-token-form__action-btn"
        >
          <VButton
            label="COPY WALLET"
            class="col-md-8 col-xs-8 col-sm-4"
            class-name="full-width"
            :disabled="!gameState.wallet"
            color="white"
            text-color="dark"
            size="lg"
            @click="handleCopyWallet"
          />
        </div>
      </div>
    </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
