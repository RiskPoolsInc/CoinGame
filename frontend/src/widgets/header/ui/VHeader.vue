<script setup lang="ts">
import { defineEmits, defineProps } from "vue/dist/vue";

import VButton from "@/shared/ui/base-components/v-button";

const props = defineProps<{
  isSidebarOpen: boolean;
}>();

const emit = defineEmits<{
  "update:isSidebarOpen": [boolean];
}>();

const toogleMenu = () => {
  emit("update:isSidebarOpen", !props.isSidebarOpen);
};
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
            <VButton label="1x_How" color="white" text-color="dark" size="lg" />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="2x_Technology"
              color="white"
              text-color="dark"
              size="lg"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="3x_Do_more"
              color="white"
              text-color="dark"
              size="lg"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="4x_Get_in_touch"
              color="white"
              text-color="dark"
              size="lg"
            />
          </div>
        </div>

        <div class="header__burger-menu my-auto">
          <VButton
            v-if="!props.isSidebarOpen"
            @click="toogleMenu"
            icon="menu"
            color="white"
            size="md"
            flat
          />

          <VButton
            v-else
            @click="toogleMenu"
            icon="close"
            color="white"
            size="md"
            flat
          />
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
