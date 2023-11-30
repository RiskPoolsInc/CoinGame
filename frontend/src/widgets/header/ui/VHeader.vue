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
    scrollToElementWithOffset(id, 115);
  }, 100);
};

const scrollToElementWithOffset = (elementId: string, offset: number) => {
  let element = document.getElementById(elementId);

  if (element) {
    let elementPosition = element.getBoundingClientRect().top;
    let start = window.pageYOffset;
    let startTime: any = null;

    const scrollAnimation = (currentTime: any) => {
      if (startTime === null) startTime = currentTime;

      let progress: any = currentTime - startTime;
      let easeInOutCubic = (progress: any) =>
        progress < 0.5
          ? 4 * progress ** 3
          : 1 - Math.pow(-2 * progress + 2, 3) / 2;

      let scrollTo = elementPosition - offset;
      window.scrollTo(0, start + scrollTo * easeInOutCubic(progress / 500));

      if (progress < 500) {
        requestAnimationFrame(scrollAnimation);
      }
    };

    requestAnimationFrame(scrollAnimation);
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
