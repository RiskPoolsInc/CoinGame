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
  <div class="do-more-card">
    <div class="do-more-card__logo">
      <img :src="getImageUrl(data.logo)" alt="" />
    </div>
    <div class="do-more-card__context">
      {{ data.context }}
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
