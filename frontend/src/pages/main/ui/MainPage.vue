<script setup lang="ts">
import InfoBlock from "@/widgets/info-blocks";
import HeroBlock from "@/widgets/hero";
import VHeader from "@/widgets/header";
import VFooter from "@/widgets/footer";

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
