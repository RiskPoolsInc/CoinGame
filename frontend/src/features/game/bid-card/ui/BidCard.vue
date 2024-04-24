<script setup lang="ts">
  import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";
  import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
  import { useGameStore } from "@/entities/game/model/game";
  import { computed, ref, watch } from "vue";
  import { MIN_BID, MAX_BID } from '../../../../../config.js'

  const { gameState, startGame } = useGameStore();

  const confirm = ref(false);
  const error = ref(false);
  let slider = ref(3);

  watch(slider, (newVal) => {
    if (newVal < 3) {
      slider.value = 3;
      return;
    }
    gameState.round = newVal;
  });

  const statusPlayButton = computed(() => {
    return !(
      Number(gameState.bid) >= MIN_BID &&
      Number(gameState.bid) <= MAX_BID &&
      gameState.round >= 3 &&
      gameState.round <= 10 &&
      gameState.wallet &&
      !gameState.inProgress
    );
  });

  const onStart = () => {
    if (Number(gameState.bid) > gameState.balance) {
      error.value = true;
      return;
    }

    confirm.value = true;
  };
</script>

<template>
  <div class="bid-card">
    <div class="row justify-center">
      <div class="col-lg-4 col-md-4 col-sm-5 col-xs-10">
        <VInput class="bid-card__input" v-model="gameState.bid" label="Your bid, UBX"
          :rules="[val => (Number(val) >= MIN_BID && Number(val) <= MAX_BID) || 'Bid should be between ' + MIN_BID + ' and ' + MAX_BID + ' UBX']" />
      </div>
    </div>

    <div class="bid-card__slider">
      <div class="bid-card__label">Number of rounds (3-10)</div>

      <q-slider v-model="slider" color="white" marker-labels :min="3" :max="10" />
    </div>

    <div class="bid-card__action row justify-center">
      <div class="col-lg-3 col-md-3 col-sm-3 col-xs-8 bid-card__btn">
        <VButton @click="onStart" :disabled="!!statusPlayButton" className="full-width" label="TOSS A COIN"
          color="white" size="lg" text-color="dark" />
      </div>
    </div>

    <q-dialog v-model="confirm" persistent>
      <q-card dark style="
          width: 491px;
          background: #50596c;
          border-radius: 7px;
          border: 1px solid #bfc9e2;
          box-shadow: none;
        " class="bid-card__dialog">
        <q-card-section class="row items-center justify-center">
          <span class="bid-card__popup-text q-ml-sm">
            The bet is placed. Start the game?
          </span>
        </q-card-section>

        <q-card-actions align="center" class="bid-card__popup-actions">
          <div class="col-lg-3 col-xs-4">
            <VButton class-name="full-width" label="Play" color="white" text-color="dark" size="lg" @click="startGame"
              v-close-popup />
          </div>

          <div class="col-lg-3 col-xs-4">
            <VButton class-name="full-width" label="Cancel" color="white" text-color="dark" size="lg"
              @click="confirm = false" v-close-popup />
          </div>
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-dialog v-model="error" persistent>
      <q-card dark style="
          width: 100%;
          max-width: 491px;
          background: #50596c;
          border-radius: 7px;
          border: 1px solid #bfc9e2;
          box-shadow: none;
        " class="bid-card__dialog">
        <q-card-section class="row items-center justify-center p-0">
          <div class="col-12 bid-card__popup-text bid-card__popup-text--error">
            <span class="bid-card__popup-text bid-card__popup-text--error">
              A bid cannot exceed the game
              <br class="xl-hide lg-hide md-hide" />
              wallet balance. <br class="xl-hide lg-hide md-hide" />
              Available range:
              <br />
              10,000 - 1,000,000 UBX
            </span>
          </div>
        </q-card-section>

        <q-card-actions align="center" class="bid-card__popup-actions">
          <div class="col-lg-3 col-xs-4">
            <VButton class-name="full-width" label="OK" color="white" text-color="dark" size="lg" v-close-popup />
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

  .q-slider__marker-labels {
    font-family: Padauk;
    font-size: 16px;
    color: #fff;
  }

  .bid-card {
    .v-input__label {
      //position: absolute;
      //margin-left: -10%;
    }
  }

  @media (max-width: 550px) {
    .bid-card {
      .v-input__label {
        margin-left: -10%;
      }
    }
  }
</style>
