<script setup lang="ts">
import { defineProps } from "vue";
import { IInfoBlock } from "@/shared/ui/home-info-block-1/model/InfoBLock.interface";
import VButton from "@/shared/ui/base-components/v-button";

interface IInfoBlockProps {
  data: IInfoBlock;
}
defineProps<IInfoBlockProps>();
</script>

<template>
  <div class="home-info-block-2">
    <div class="container">
      <div class="home-info-block-2__title" v-html="data.title" />
      <div class="home-info-block-2__body row justify-between">
        <div class="home-info-block-2__content col-lg-7 col-md-7 col-sm-12">
          <p>
            In this classic children’s game, one player tosses a coin and covers
            it with their hand – while the other has to guess if it’s heads or
            tails. In our scenario, the user plays against a risk pool. Guess
            right – and you’ll get the reward from the pool; guess wrong – and
            the reward will go to the pool holder. <br />
            But all is not as simple as it seems…
          </p>
        </div>
        <img
          class="home-info-block-2__img"
          src="./images/home-info-block-2.png"
          alt="info-block-2"
        />
        <VButton
          class="col-lg-3 col-md-4 home-info-block-2__btn"
          size="lg"
          label="FULL EXAMPLE"
          color="white"
          text-color="dark"
          padding="9px 59px"
          to="/example"
        />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
