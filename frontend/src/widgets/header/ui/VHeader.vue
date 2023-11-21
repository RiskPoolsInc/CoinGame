<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
</script>

<template>
  <header class="main-header">
    <div class="header">
      <div class="header__wrapper container">
        <div class="header__logo my-auto">
          <img class="" src="./images/svg/logo.svg" alt="logo" />

          <img
            class="header__title my-auto"
            src="./images/svg/text.svg"
            alt="text"
          />
        </div>
        <div class="header__menu">
          <div class="header__menu__item my-auto">
            <VButton label="1x_How" color="white" text-color="dark" size="sm" />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="2x_Technology"
              color="white"
              text-color="dark"
              size="sm"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="3x_Do_more"
              color="white"
              text-color="dark"
              size="sm"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="4x_Get_in_touch"
              color="white"
              text-color="dark"
              size="sm"
            />
          </div>
        </div>

        <div class="header__burger-menu my-auto">
          <VButton icon="menu" color="white" size="xl" flat />
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
