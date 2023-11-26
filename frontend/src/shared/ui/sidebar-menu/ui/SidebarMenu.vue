<script setup lang="ts">
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { defineEmits, defineProps } from "vue";

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
  <div class="sidebar-menu">
    <div class="sidebar-menu__head">
      <img class="sidebar-menu__logo" src="./images/svg/logo.svg" alt="logo" />

      <VButton icon="close" color="white" size="lg" flat @click="toogleMenu" />
    </div>
    <ul class="sidebar-menu__list">
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game">1x_How</router-link>
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >2x_Technology</router-link
        >
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >3x_Do_more</router-link
        >
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >4x_Get_in_touch</router-link
        >
      </li>
    </ul>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
