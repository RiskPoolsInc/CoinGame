<script setup lang="ts">
import { BLOCKS } from "@/widgets/info-blocks/model/constants";
import HomeInfoBlock3 from "@/shared/ui/home-info-block-3/ui/HomeInfoBlock3.vue";
import HomeInfoBlock1 from "@/shared/ui/home-info-block-1/ui/HomeInfoBlock1.vue";
import HomeInfoBlock2 from "@/shared/ui/home-info-block-2/ui/HomeInfoBlock2.vue";
</script>

<template>
  <div>
    <HomeInfoBlock1 :data="BLOCKS[0]" />

    <HomeInfoBlock2 :data="BLOCKS[1]" />

    <home-info-block3 :data="BLOCKS[2]" />
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
