<script setup lang="ts">
import { defineProps, withDefaults} from "vue";

interface IVButtonProps {
  color?: string;
  label?: string;
  icon?: string;
  iconRight?: string;
  size?: "xs" | "sm" | "md" | "lg" | "xl";
  textColor?: string;
  flat?: boolean;
  to?: string;
  customStyle?: unknown;
  disabled?: boolean;
  className?: string;
  outline?: boolean;
  padding?: string;
  target?: string;
  href?: string
}

withDefaults(defineProps<IVButtonProps>(), {
  color: "primary",
  label: undefined,
  icon: undefined,
  iconRight: undefined,
  size: "md",
  customStyle: undefined,
  padding: undefined,
  target: undefined,
});
</script>

<template>
    <q-btn
      :outline="outline"
      :color="color"
      :padding="padding"
      :to="to"
      :href="href"
      :icon="icon"
      :target="target"
      :flat="flat"
      :icon-right="iconRight"
      :label="label"
      :size="size"
      :textColor="textColor"
      :style="customStyle"
      :disable="disabled"
      :class="className"
    >
      <slot></slot>
    </q-btn>
</template>

<style scoped></style>
