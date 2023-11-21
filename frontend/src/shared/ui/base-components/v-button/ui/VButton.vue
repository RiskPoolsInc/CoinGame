<script setup lang="ts">
import { defineProps, withDefaults } from "vue";

interface IVButtonProps {
  color?: string;
  label?: string;
  icon?: string;
  iconRight?: string;
  size?: "xs" | "sm" | "md" | "lg" | "xl";
  textColor?: string;
  flat?: boolean;
  to?: string;
}
withDefaults(defineProps<IVButtonProps>(), {
  color: "primary",
  label: undefined,
  icon: undefined,
  iconRight: undefined,
  size: "md",
});
</script>

<template>
  <div>
    <q-btn
      :color="color"
      :to="to"
      :icon="icon"
      :flat="flat"
      :icon-right="iconRight"
      :label="label"
      :size="size"
      :textColor="textColor"
    />
  </div>
</template>

<style scoped></style>
