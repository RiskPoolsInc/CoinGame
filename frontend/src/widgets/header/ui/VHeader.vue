<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
</script>

<template>
  <header>
    <div class="header">
      <div class="header__logo">
        <img src="./images/svg/logo.svg" alt="logo" />
      </div>
      <div class="header__menu">
        <div class="header__menu__item">
          <VButton>Home</VButton>
        </div>
        <div class="header__menu__item">
          <VButton>Home</VButton>
        </div>
        <div class="header__menu__item">
          <VButton>Home</VButton>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
