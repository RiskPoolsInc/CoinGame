<script setup lang="ts">
import ExampleInfoBlock from "@/shared/ui/example-info-block";
import ExampleInfoSub from "@/shared/ui/example-info-sub";
import { EXAMPLE_INFO_SUB_LIST } from "@/shared/ui/example-info-sub/model/constants";
</script>

<template>
  <div class="example-page">
    <ExampleInfoBlock />

    <ExampleInfoSub :list="EXAMPLE_INFO_SUB_LIST" />
  </div>
</template>
<style scoped lang="scss">
@import "styles.module";
</style>
