<script setup lang="ts">
import HeroBlock from "@/widgets/hero";
import InfoBlocks from "@/widgets/info-blocks";
import DoMore from "@/widgets/do-more/ui/DoMore.vue";
import GetInTouch from "@/widgets/get-in-touch";

console.log("Main Page");
</script>

<template>
  <div>
    <!--  Hero  -->
    <HeroBlock />

    <!--  Info Block  -->
    <InfoBlocks />

    <!--  Do More  -->
    <DoMore />

    <!--  Get In Touch  -->
    <GetInTouch />
  </div>
</template>
