<script setup lang="ts">
import { defineProps } from "vue";
import { IInfoBlock } from "@/shared/ui/home-info-block-1/model/InfoBLock.interface";

interface IInfoBlockProps {
  data: IInfoBlock;
}
defineProps<IInfoBlockProps>();
</script>

<template>
  <div class="home-info-block-2">
    <div class="container">
      <div class="home-info-block-2__title">
        {{ data.title }}
      </div>
      <div class="home-info-block-2__body">
        <div class="home-info-block-2__content">
          <p>
            In this classic children’s game, one player tosses a coin and covers
            it with their hand – while the other has to guess if it’s heads or
            tails. In our scenario, the user plays against a risk pool. Guess
            right – and you’ll get the reward from the pool; guess wrong – and
            the reward will go to the pool holder. But all is not as simple as
            it seems…
          </p>
        </div>
        <div class="home-info-block-2__img">
          <img src="./images/info-block-img.png" alt="info-block-2" />
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
