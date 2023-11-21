<script setup lang="ts">
import { defineProps } from "vue";

import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";

interface ISubInfo {
  title: string;
  context: string;
  btnName: string;
  btnLink: string;
}
interface IExampleInfoSubProps {
  list: ISubInfo[];
}
defineProps<IExampleInfoSubProps>();
</script>

<template>
  <div class="example-info-sub">
    <div class="container">
      <div class="row">
        <div class="col-lg-7 example-info-sub__title">
          112x_How to participate in RiskPools
        </div>
      </div>

      <div
        v-for="(block, index) in list"
        :key="index"
        class="example-info-sub__content row"
      >
        <div class="col-lg-12 row justify-between">
          <div class="col-lg-3 example-info-sub__sub-title">
            {{ block.title }}
          </div>

          <div class="col-lg-3 text-right">
            <VButton
              :label="block.btnName"
              :to="block.btnLink"
              class="example-info-sub__btn"
              text="Play now"
              type="primary"
              size="large"
              color="white"
              text-color="dark"
            />
          </div>
        </div>

        <p class="example-info-sub__context" v-html="block.context"></p>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
