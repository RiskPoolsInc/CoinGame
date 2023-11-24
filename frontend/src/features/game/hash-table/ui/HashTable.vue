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
  <q-markup-table dark>
    <thead>
      <tr>
        <th v-for="(column, index) in columns" :key="index" class="text-left">
          {{ column.label }}
        </th>
      </tr>
    </thead>

    <tbody>
      <tr v-for="(row, index) in rows" :key="index">
        <td v-for="(column, index2) in columns" :key="index2" class="text-left">
          {{ row[column.field] }}
        </td>
      </tr>
    </tbody>
  </q-markup-table>
</template>

<style lang="scss">
@import "./styles.module";
</style>
