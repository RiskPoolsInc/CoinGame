<script setup lang="ts">
// import { computed } from "vue";
//
// type VueSelectProps = {
//   data: any;
//   modelValue: any | null;
//   label?: string;
//   className?: string;
//   textProperty?: string;
//   withDefaultOption?: boolean;
// };
//
// const props = withDefaults(defineProps<VueSelectProps>(), {
//   textProperty: "label",
//   withDefaultOption: true,
// });
//
// const emit = defineEmits(["update:modelValue"]);
//
// const value = computed<any | null>({
//   get: () => props.modelValue,
//   set: (val) => {
//     emit("update:modelValue", val);
//   },
// });
</script>

<template>
  <div>
    <q-btn
      color="red"
      icon="mail"
      icon-right="send"
      label="On Left and Right"
    />
  </div>
</template>

<style scoped></style>
