<script setup lang="ts">
import InfoBlock from "@/widgets/info-blocks";
import HeroBlock from "@/widgets/hero";
import VHeader from "@/widgets/header";
import VFooter from "@/widgets/footer";
import DoMore from "@/widgets/do-more/ui/DoMore.vue";
import GetInTouch from "@/widgets/get-in-touch/ui/GetInTouch.vue";

console.log("Main Page");
</script>

<template>
  <div>
    <!--  Header  -->
    <VHeader />

    <!--  Hero  -->
    <HeroBlock />

    <!--  Info Block  -->
    <InfoBlock />

    <!--  Do More  -->
    <DoMore />

    <!--  Get In Touch  -->
    <GetInTouch />

    <!--  Footer  -->
    <VFooter />
  </div>
</template>
