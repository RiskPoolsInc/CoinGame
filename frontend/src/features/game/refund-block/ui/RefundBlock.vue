<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";

const { generalReset } = useGameStore();
</script>

<template>
  <div class="refund-block">
    <div class="row justify-between">
      <div class="col-lg-9">
        <div class="refund-block__title">112PxC_Claim the winnings</div>

        <div class="refund-block__description">
          To claim the funds back to your original address, click the button
          below. If you forget or close the page, or if the transfer doesn’t
          work due to technical issues, the game wallet balance will be refunded
          automatically within 24 hours.
        </div>
      </div>

      <div class="col-lg-2 col-xs-12 my-auto refund-block__actions">
        <VButton
          class="refund-block__btn"
          label="REFUND"
          color="white"
          text-color="dark"
          size="lg"
          className="full-width"
          @click="generalReset"
        />

        <VButton
          to="/check-game"
          class="refund-block__btn"
          label="CHECK GAME"
          color="white"
          target="_blank"
          text-color="dark"
          size="lg"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
