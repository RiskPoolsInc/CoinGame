<script setup lang="ts">
import { ref } from "vue";
import VInput from "@/shared/ui/base-components/v-input/ui/VInput.vue";

const a = ref("");
</script>

<template>
  <div class="result-game">
    <div class="row justify-end">
      <div class="col-lg-6">
        <VInput v-model="a" left-label label="Your game result, UBX" />
      </div>

      <div class="col-lg-6">
        <VInput v-model="a" left-label label="Your game wallet balance, UBX" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>
