<script setup lang="ts">
import { defineProps, withDefaults } from "vue";

interface IVButtonProps {
  color?: string;
  label?: string;
  icon?: string;
  iconRight?: string;
  size?: "xs" | "sm" | "md" | "lg" | "xl";
  textColor?: string;
}
withDefaults(defineProps<IVButtonProps>(), {
  color: "primary",
  label: "Button",
  icon: undefined,
  iconRight: undefined,
  size: "md",
});
</script>

<template>
  <div>
    <q-btn
      :color="color"
      :icon="icon"
      :icon-right="iconRight"
      :label="label"
      :size="size"
      :textColor="textColor"
    />
  </div>
</template>

<style scoped></style>
