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
  <div class="row hash-table-2 justify-end">
    <q-markup-table class="desktop" dark>
      <thead>
        <tr>
          <th v-for="(column, index) in columns" :key="index" class="text-left">
            {{ column.label }}
          </th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="index in 5" :key="index">
          <td>
            {{ index }}
          </td>

          <td>
            {{ rows[index - 1]?.number }}
          </td>

          <td>
            <span v-if="rows[index - 1]">
              {{ rows[index - 1]?.parity ? "OK-WIN" : "NONE-LOSE" }}
            </span>
          </td>
        </tr>
      </tbody>
    </q-markup-table>

    <q-markup-table class="desktop" dark>
      <thead>
        <tr>
          <th v-for="(column, index) in columns" :key="index" class="text-left">
            {{ column.label }}
          </th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="index in 10" :key="index">
          <td v-if="index > 5">
            {{ index }}
          </td>

          <td v-if="index > 5">
            {{ rows[index - 1]?.number }}
          </td>

          <td v-if="index > 5">
            <span v-if="rows[index - 1]">
              {{ rows[index - 1]?.parity ? "OK-WIN" : "NONE-LOSE" }}
            </span>
          </td>
        </tr>
      </tbody>
    </q-markup-table>

    <q-markup-table class="mobile" dark>
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

          <td>
            {{ rows[index - 1]?.number }}
          </td>

          <td>
            <span v-if="rows[index - 1]">
              {{ rows[index - 1]?.parity ? "OK-WIN" : "NONE-LOSE" }}
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
