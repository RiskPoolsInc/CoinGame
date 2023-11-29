<script setup lang="ts">
import { defineEmits, defineProps } from "vue";
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

  toogleMenu();
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
  <div class="sidebar-menu">
    <ul class="sidebar-menu__list">
      <li class="sidebar-menu__item">
        <div class="sidebar-menu__link" @click="scrollTo('how')">1x_How</div>
      </li>
      <li class="sidebar-menu__item">
        <div class="sidebar-menu__link" @click="scrollTo('technology')">
          2x_Technology
        </div>
      </li>
      <li class="sidebar-menu__item">
        <div class="sidebar-menu__link" @click="scrollTo('do-more')">
          3x_Do_more
        </div>
      </li>
      <li class="sidebar-menu__item">
        <div class="sidebar-menu__link" @click="scrollTo('get-in-touch')">
          4x_Get_in_touch
        </div>
      </li>
    </ul>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
