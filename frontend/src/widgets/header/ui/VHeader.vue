<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
</script>

<template>
  <header>
    <div class="header">
      <div class="header__wrapper container">
        <div class="header__logo my-auto">
          <img src="./images/svg/logo.svg" alt="logo" />
        </div>
        <div class="header__menu">
          <div class="header__menu__item my-auto">
            <VButton label="HOW" color="white" text-color="primary" size="sm" />
          </div>
          <div class="header__menu__item my-auto">
            <VButton
              label="TECHNOLOGY"
              color="white"
              text-color="primary"
              size="sm"
            />
          </div>
          <div class="header__menu__item my-auto">
            <VButton
              label="DO MORE"
              color="white"
              text-color="primary"
              size="sm"
            />
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
