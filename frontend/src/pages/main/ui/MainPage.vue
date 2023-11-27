<script setup lang="ts">
import HeroBlock from "@/widgets/hero";
import InfoBlocks from "@/widgets/info-blocks";
import DoMore from "@/widgets/do-more/ui/DoMore.vue";
import GetInTouch from "@/widgets/get-in-touch";
</script>

<template>
  <div class="main-page">
    <!--  Hero  -->
    <section>
      <HeroBlock />
    </section>

    <section id="how">
      <!--  Info Block  -->
      <InfoBlocks />
    </section>

    <section id="do-more">
      <!--  Do More  -->
      <DoMore />
    </section>

    <section id="get-in-touch">
      <!--  Get In Touch  -->
      <GetInTouch />
    </section>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
