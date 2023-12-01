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
  <div class="hash-table">
    <q-markup-table dark>
      <thead>
        <tr>
          <th v-for="(column, index) in columns" :key="index" class="text-left">
            {{ column.label }}
          </th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="index in 10" :key="index">
          <td>
            {{ index }}
          </td>
          <td class="ellipsis">
            <span>
              {{ rows[index - 1]?.hashNumber }}
            </span>
          </td>
        </tr>
      </tbody>
    </q-markup-table>
  </div>
</template>

<style lang="scss">
@import "./styles.module";
</style>
