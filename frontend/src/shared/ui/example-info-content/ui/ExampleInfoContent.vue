<template>
  <div
      class="example-info-content row"
  >
    <div class="col-lg-12 col-md-12 col-sm-12 row justify-between">
      <div class="col-lg-3 col-md-6 example-info-content__title">
        {{ title }}
      </div>

      <div class="col-lg-2 col-md-3 col-sm-4 text-right">
        <VButton
            :label="btnName"
            :to="btnLink"
            class="example-info-content__button--desktop"
            text="Play now"
            type="primary"
            size="lg"
            color="white"
            class-name="full-width"
            text-color="dark"
        />
      </div>
    </div>

    <div class="col-lg-9 example-info-content__context">
      <slot/>
    </div>

    <VButton
        :label="btnName"
        :to="btnLink"
        class="example-info-content__button--mobile"
        text="Play now"
        type="primary"
        class-name="full-width"
        size="lg"
        color="white"
        text-color="dark"
    />
  </div>
</template>

<script lang="ts" setup>
import VButton from "@/shared/ui/base-components/v-button";


type Props = {
  title: string,
  btnName: string
  btnLink: string
}

defineProps<Props>()
</script>

<style scoped lang="scss">
@import "./styles.module";
</style>
