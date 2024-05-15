<template>
  <q-linear-progress class="q-progress" size="25px" query color="white" stripe reverse track-color="positive">
    <div class="absolute-full flex flex-center">
      <q-badge color="white" text-color="dark" class="q-progress__label" v-bind="{label}" />
    </div>
  </q-linear-progress>
</template>

<script lang="ts" setup>
type Props = {
  label: string
}

defineProps<Props>()
</script>
<style lang="scss" scoped>
.q-progress {
  font-size: 16px;

  &__label {
    @media (max-width: 550px) {
      font-size: 10px;
    }
  }
}
</style>
