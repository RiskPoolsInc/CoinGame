<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
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

      <div class="col-lg-1 my-auto">
        <VButton label="REFUND" color="white" text-color="dark" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
