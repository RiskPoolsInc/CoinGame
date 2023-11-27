<script setup lang="ts">
import { BLOCKS } from "@/widgets/info-blocks/model/constants";
import HomeInfoBlock1 from "@/shared/ui/home-info-block-1";
import HomeInfoBlock2 from "@/shared/ui/home-info-block-2/ui/HomeInfoBlock2.vue";
import HomeInfoBlock3 from "@/shared/ui/home-info-block-3/ui/HomeInfoBlock3.vue";
</script>

<template>
  <div class="home-info-blocks">
    <HomeInfoBlock1 :data="BLOCKS[0]" />

    <HomeInfoBlock2 :data="BLOCKS[1]" />

    <home-info-block3 id="technology" :data="BLOCKS[2]" />
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
