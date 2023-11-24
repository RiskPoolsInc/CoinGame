<script setup lang="ts">
import { computed, defineEmits, defineProps, withDefaults } from "vue";

type InputValueType = string | number | undefined;
interface IVButtonProps {
  label?: string;
  modelValue: InputValueType;
  topLabel?: boolean;
  leftLabel?: boolean;
  disabled?: boolean;
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
      class="v-input__input"
      outlined
      :disable="disabled"
      v-model="inputValue"
      autocomplete="off"
      dark
      lazy-rules
    />
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
<style lang="scss">
.q-field--outlined .q-field__control {
  border-radius: 7px;
}
</style>
