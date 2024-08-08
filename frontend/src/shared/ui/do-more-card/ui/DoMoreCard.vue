<script setup lang="ts">
import { defineProps } from "vue";
import { IDoMoreCard } from "@/widgets/do-more/modules/doMore.interface";

interface IGetInTouchCardProps {
  data: IDoMoreCard;
}
defineProps<IGetInTouchCardProps>();

const getImageUrl = (url: string) => {
  return require(`./images/${url}`);
};
</script>

<template>
  <a :href="data.link" class="do-more-card" target="_blank">
    <div class="do-more-card__logo">
      <img :src="getImageUrl(data.logo)" alt="" />
    </div>
    <div class="do-more-card__context" v-html="$sanitize(data.context)" />
  </a>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
