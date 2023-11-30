<script setup lang="ts">
import GetInTouchCard from "@/shared/ui/do-more-card";
import { DO_MORE_LIST } from "@/widgets/do-more/modules/constants";
</script>

<template>
  <div class="do-more">
    <div class="do-more__title">3x_Do more with RiskPools</div>
    <div class="do-more__list row justify-between items-baseline">
      <GetInTouchCard
        v-for="(data, index) in DO_MORE_LIST"
        :key="index"
        :data="data"
        class="col-lg-3 col-md-4 col-sm-12 col-xs-8"
      />
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
