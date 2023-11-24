<script setup lang="ts">
import { defineProps, withDefaults } from "vue/dist/vue";

interface IVChipProps {
  color?:
    | "success"
    | "danger"
    | "warning"
    | "primary"
    | "secondary"
    | "info"
    | "dark"
    | "light";
  size?: "xs" | "sm" | "md" | "lg" | "xl";
  textColor?: string;
}

withDefaults(defineProps<IVChipProps>(), {
  color: "success",
  size: "md",
});
</script>

<template>
  <div :class="`v-chip v-chip--${color}`">
    <div class="v-chip__content">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
