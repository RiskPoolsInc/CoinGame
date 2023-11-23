<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button";
</script>

<template>
  <div class="hero-block">
    <div class="hero-block__main">
      <img
        class="hero-block__main--desktop"
        src="./images/riskpools.svg"
        alt="main text"
      />
      <img
        class="hero-block__main--mobile"
        src="./images/hero-bg-mobile.svg"
        alt="main text"
      />
    </div>
    <div class="hero-block__sub container">
      <div class="hero-block__context">
        Make money taking up the risks of others – or hedge your own. RiskPools
        is the ultimate risk management and monetization protocol.
      </div>

      <div class="hero-block__action-btn my-auto">
        <VButton
          label="START EARNING"
          icon-right="arrow_forward"
          color="white"
          text-color="dark"
          to="/example"
          size="lg"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
