<script setup lang="ts">
import { computed, defineEmits, defineProps, withDefaults } from "vue";

type InputValueType = string | number | undefined;
interface IVButtonProps {
  label?: string;
  modelValue: InputValueType;
  topLabel?: boolean;
  leftLabel?: boolean;
}
const props = withDefaults(defineProps<IVButtonProps>(), {
  label: undefined,
  topLabel: undefined,
});

const emit = defineEmits<{
  "update:modelValue": [InputValueType];
}>();

const inputValue = computed({
  get: () => props.modelValue,
  set: (val) => emit("update:modelValue", val),
});
</script>

<template>
  <div
    class="v-input"
    :class="[
      {
        'top-label': topLabel,
        'left-label': leftLabel,
      },
    ]"
  >
    <label class="v-input__label"> {{ label }} </label>
    <q-input
      outlined
      v-model="inputValue"
      autocomplete="off"
      dark
      dense
      lazy-rules
    />
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
