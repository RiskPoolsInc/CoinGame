<script setup lang="ts">
import { Routing } from "@/pages/routing";
import VHeader from "@/widgets/header";
import VFooter from "@/widgets/footer/ui/VFooter.vue";

console.log("Main App enter point");
</script>

<template>
  <div>
    <!--  Header  -->
    <VHeader />

    <Routing />

    <!--  Footer  -->
    <VFooter />
  </div>
</template>
