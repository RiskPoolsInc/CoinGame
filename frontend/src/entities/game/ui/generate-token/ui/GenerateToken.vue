<script setup lang="ts">
import GenerateTokenForm from "@/features/game/generate-token-form/ui/GenerateTokenForm.vue";
</script>

<template>
  <div class="generate-token">
    <div class="generate-token__title">112PxA_Top up the wallet</div>

    <div class="generate-token__info">
      To participate in the game you need to top up your game wallet. Generate a
      wallet and top it up.
    </div>

    <GenerateTokenForm />
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
