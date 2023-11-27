<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
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
    const el = document.getElementById(id);
    if (el) {
      el.scrollIntoView({ behavior: "smooth" });
    }
  }, 100);

  toogleMenu();
};
</script>

<template>
  <div class="sidebar-menu">
    <div class="sidebar-menu__head">
      <img class="sidebar-menu__logo" src="./images/svg/logo.svg" alt="logo" />

      <VButton icon="close" color="white" size="lg" flat @click="toogleMenu" />
    </div>
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
