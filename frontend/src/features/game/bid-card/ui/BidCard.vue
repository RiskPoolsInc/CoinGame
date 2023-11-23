<script setup lang="ts">
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";
import { computed, ref } from "vue";

const { gameState, startGame } = useGameStore();

const confirm = ref(false);

const statusPlayButton = computed(() => {
  return !(Number(gameState.bid) > 10000 && gameState.round >= 3);
});
</script>

<template>
  <div class="bid-card">
    <div class="row justify-center">
      <div class="col-lg-3">
        <VInput v-model="gameState.bid" label="Your bid, UBX" />
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
          @click="confirm = true"
          :disabled="!!statusPlayButton"
          className="full-width"
          label="TOSS A COIN"
          color="white"
          text-color="dark"
        />
      </div>
    </div>

    <q-dialog v-model="confirm" persistent>
      <q-card dark>
        <q-card-section class="row items-center">
          <span class="bid-card__popup-text q-ml-sm"
            >The bet is placed. Start the game?</span
          >
        </q-card-section>

        <q-card-actions align="around">
          <VButton
            label="Play"
            color="white"
            text-color="dark"
            @click="startGame"
            v-close-popup
          />

          <VButton
            @click="confirm = false"
            label="Cancel"
            color="white"
            text-color="dark"
            v-close-popup
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
