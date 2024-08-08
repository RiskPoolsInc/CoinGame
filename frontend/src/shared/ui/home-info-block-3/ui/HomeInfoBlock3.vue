<script setup lang="ts">
import { defineProps } from "vue";
import { IInfoBlock } from "@/shared/ui/home-info-block-1/model/InfoBLock.interface";

interface IInfoBlockProps {
  data: IInfoBlock;
}
defineProps<IInfoBlockProps>();
</script>

<template>
  <div class="home-info-block-3">
    <div class="container">
      <div class="home-info-block-3__head-wrapper">
        <div class="home-info-block-3__title">
          {{ data.title }}
        </div>
      </div>
      <div class="home-info-block-3__body">
        <img
          class="home-info-block-3__img"
          src="./images/home-info-blog-3.png"
          alt="info-block-3"
        />

        <div class="home-info-block-3__content">
          <p>
            Protocol revenue is distributed as follows: if a player loses, 20% of their bid is deposited in the risk pool to boost token backing. The remaining 80% is distributed among the pool’s stakers via Ubistake.io.
            <br /><br />
            Risk pools use smart contracts residing in a separate consilium (essentially our own blockchain) built on the UBIX.Network DAG. This is a very flexible technology; our only limitation is the shared address space.
            <br /><br />
            Each pool will have its own token (similar to LP tokens on a DEX), issued using UBIX.Network’s native T-10 standard. This is necessary to record each pool holder’s share and the corresponding share of the revenue. Pool profits will be distributed via Ubistake.io.
            <br /><br />
            We will balance the pools using probability fields and non-ergodic random processes. The notion of a random value or process will be formulated individually for each pool. All related documents will be posted on GitHub.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
