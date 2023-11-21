<script setup lang="ts">
import HeroBlock from "@/widgets/hero";
import VHeader from "@/widgets/header";
import InfoBlocks from "@/widgets/info-blocks";
import DoMore from "@/widgets/do-more/ui/DoMore.vue";
import GetInTouch from "@/widgets/get-in-touch";
import VFooter from "@/widgets/footer/ui/VFooter.vue";

console.log("Main Page");
</script>

<template>
  <div>
    <!--  Header  -->
    <VHeader />

    <!--  Hero  -->
    <HeroBlock />

    <!--  Info Block  -->
    <InfoBlocks />

    <!--  Do More  -->
    <DoMore />

    <!--  Get In Touch  -->
    <GetInTouch />

    <!--  Footer  -->
    <VFooter />
  </div>
</template>
