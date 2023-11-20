<script setup lang="ts">
import VFooter from "@/shared/ui/footer/ui/VFooter.vue";
import InfoBlock from "@/widgets/info-blocks/ui/InfoBlocks.vue";
import HeroBlock from "@/widgets/hero/ui/HeroBlock.vue";
import VHeader from "@/widgets/header/ui/VHeader.vue";

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

    <!--  Footer  -->
    <VFooter />
  </div>
</template>
