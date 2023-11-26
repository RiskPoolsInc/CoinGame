<script setup lang="ts">
import { Routing } from "@/pages/routing";
import VHeader from "@/widgets/header";
import VFooter from "@/widgets/footer/ui/VFooter.vue";
import { ref } from "vue";
import SidebarMenu from "@/shared/ui/sidebar-menu/ui/SidebarMenu.vue";

const isSidebarOpen = ref(false);
</script>

<template>
  <div>
    <!--  Header  -->
    <VHeader v-model:isSidebarOpen="isSidebarOpen" />

    <!--  Sidebar menu  -->
    <SidebarMenu v-if="isSidebarOpen" v-model:isSidebarOpen="isSidebarOpen" />

    <!--  Pages  -->
    <Routing />

    <!--  Footer  -->
    <VFooter />
  </div>
</template>
