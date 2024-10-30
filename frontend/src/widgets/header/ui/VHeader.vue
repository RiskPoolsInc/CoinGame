<script setup lang="ts">
import { defineEmits, defineProps } from "vue/dist/vue";

import VButton from "@/shared/ui/base-components/v-button";
import { router } from "@/app/providers";

const props = defineProps<{
  isSidebarOpen: boolean;
}>();

const emit = defineEmits<{
  "update:isSidebarOpen": [boolean];
}>();

const toogleMenu = () => {
  emit("update:isSidebarOpen", !props.isSidebarOpen);
};

const openMainPageOrScrollToTop = () => {
  if (router.currentRoute.value.path !== "/") {
    router.replace({ name: "home" });
    return;
  }

  console.log("openMainPageOrScrollToTop");

  setTimeout(() => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  }, 100);
};
</script>

<template>
  <header class="main-header">
    <div class="header">
      <div class="header__wrapper container">
        <div class="header__logo my-auto" @click="openMainPageOrScrollToTop">
          <img class="" src="./images/svg/logo.svg" alt="logo" />

          <img
            class="header__title my-auto"
            src="./images/svg/text.svg"
            alt="text"
          />
        </div>
        <div class="header__menu">
          <div class="header__menu__item my-auto">
            <VButton
              label="1x_How"
              color="white"
              text-color="dark"
              size="lg"
              class-name="header__how"
              href="/#how"
              tabindex="0"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="2x_Technology"
              color="white"
              text-color="dark"
              size="lg"
              tabindex="0"
              class-name="header__technology"
              href="/#technology"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="3x_Do_more"
              color="white"
              class-name="header__do-more"
              text-color="dark"
              size="lg"
              tabindex="0"
              href="/#do-more"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="4x_Get_in_touch"
              color="white"
              class-name="header__get-in-touch"
              text-color="dark"
              size="lg"
              tabindex="0"
              href="/#get-in-touch"
            />
          </div>
        </div>

        <div class="header__burger-menu my-auto">
          <div
            v-if="!props.isSidebarOpen"
            class="header__menu-icon"
            @click="toogleMenu"
          >
            <img src="./images/svg/reorder-two-sharp.svg" alt="" srcset="" />
          </div>

          <div v-else class="header__close-icon" @click="toogleMenu">
            <img src="" alt="" srcset="./images/svg/close-sharp.svg" />
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
