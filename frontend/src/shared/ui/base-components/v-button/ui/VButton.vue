<script setup lang="ts">
import { defineEmits, defineProps, withDefaults } from "vue";

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

const emit = defineEmits<{
  click: [void];
}>();

const handleClick = () => {
  emit("click");
};
</script>

<template>
  <div>
    <q-btn
      :outline="outline"
      :color="color"
      :padding="padding"
      :to="to"
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
      @click="handleClick"
    >
      <slot></slot>
    </q-btn>
  </div>
</template>

<style scoped></style>
