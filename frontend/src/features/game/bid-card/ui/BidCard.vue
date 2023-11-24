<script setup lang="ts">
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";
import { computed, ref } from "vue";

const { gameState, startGame } = useGameStore();

const confirm = ref(false);

const statusPlayButton = computed(() => {
  return !(
    Number(gameState.bid) > 10000 &&
    gameState.round >= 3 &&
    gameState.wallet
  );
});
</script>

<template>
  <div class="bid-card">
    <div class="row justify-center">
      <div class="col-lg-4">
        <VInput
          class="bid-card__input"
          v-model="gameState.bid"
          label="Your bid, UBX"
        />
      </div>
    </div>

    <div class="bid-card__slider">
      <div class="bid-card__label">Number of rounds (3-10)</div>

      <q-slider
        v-model="gameState.round"
        :inner-min="3"
        color="white"
        marker-labels
        :min="0"
        :max="10"
      />
    </div>

    <div class="bid-card__action row justify-center">
      <div class="col-lg-3">
        <VButton
          className="full-width"
          label="BID"
          color="white"
          size="lg"
          text-color="dark"
        />
      </div>

      <div class="col-lg-3">
        <VButton
          @click="confirm = true"
          :disabled="!!statusPlayButton"
          className="full-width"
          label="TOSS A COIN"
          color="white"
          size="lg"
          text-color="dark"
        />
      </div>
    </div>

    <q-dialog v-model="confirm" persistent>
      <q-card
        dark
        style="
          width: 491px;
          background: #50596c;
          border-radius: 7px;
          border: 1px solid #bfc9e2;
          box-shadow: none;
        "
        class="bid-card__dialog"
      >
        <q-card-section class="row items-center justify-center">
          <span class="bid-card__popup-text q-ml-sm">
            The bet is placed. Start the game?
          </span>
        </q-card-section>

        <q-card-actions align="center" class="bid-card__popup-actions">
          <div class="col-3">
            <VButton
              class-name="full-width"
              label="Play"
              color="white"
              text-color="dark"
              size="lg"
              @click="startGame"
              v-close-popup
            />
          </div>

          <div class="col-3">
            <VButton
              class-name="full-width"
              label="Cancel"
              color="white"
              text-color="dark"
              size="lg"
              @click="confirm = false"
              v-close-popup
            />
          </div>
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
<style lang="scss">
.bid-card__input {
  .q-field--dark .q-field__control:before {
    border: 3px solid #bfc9e2;
  }
}
</style>
