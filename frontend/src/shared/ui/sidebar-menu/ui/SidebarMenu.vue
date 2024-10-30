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
</script>

<template>
  <div class="sidebar-menu">
    <ul class="sidebar-menu__list">
      <li class="sidebar-menu__item">
        <a class="sidebar-menu__link" href="/#how" @click="toogleMenu">1x_How</a>
      </li>
      <li class="sidebar-menu__item">
        <a class="sidebar-menu__link" href="/#technology" @click="toogleMenu">
          2x_Technology
        </a>
      </li>
      <li class="sidebar-menu__item">
        <a class="sidebar-menu__link" href="/#do-more"  @click="toogleMenu">
          3x_Do_more
        </a>
      </li>
      <li class="sidebar-menu__item">
        <a class="sidebar-menu__link" href="/#get-in-touch"  @click="toogleMenu">
          4x_Get_in_touch
        </a>
      </li>
    </ul>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
