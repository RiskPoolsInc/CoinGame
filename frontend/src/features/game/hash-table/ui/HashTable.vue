<script setup lang="ts">
import { defineProps } from "vue";

defineProps({
  rows: {
    type: Array,
    required: true,
  },
  columns: {
    type: Array,
    required: true,
  },
});
</script>

<template>
  <div class="">
    <q-table
      flat
      bordered
      :rows="rows"
      :columns="columns"
      hide-bottom
      dark
      row-key="name"
    />
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
