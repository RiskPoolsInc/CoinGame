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

const scrollTo = (id: string) => {
  if (router.currentRoute.value.path !== "/") {
    router.replace({ name: "home", hash: "#" + id });
    // return;
  }

  setTimeout(() => {
    const el = document.getElementById(id);
    if (el) {
      el.scrollIntoView({ behavior: "smooth" });
    }
  }, 100);
};

const redirectWithHash = () => {
  if (router.currentRoute.value.path !== "/") {
    router.push({ name: "home", hash: "how" });
  }
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
            <VButton
              label="1x_How"
              color="white"
              text-color="dark"
              size="lg"
              @click="scrollTo('how')"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="2x_Technology"
              color="white"
              text-color="dark"
              size="lg"
              @click="scrollTo('technology')"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="3x_Do_more"
              color="white"
              text-color="dark"
              size="lg"
              @click="scrollTo('do-more')"
            />
          </div>

          <div class="header__menu__item my-auto">
            <VButton
              label="4x_Get_in_touch"
              color="white"
              text-color="dark"
              size="lg"
              @click="scrollTo('get-in-touch')"
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
