<script setup lang="ts">
import { defineProps } from "vue";
import { IInfoBlock } from "@/shared/ui/home-info-block-1/model/InfoBLock.interface";

interface IInfoBlockProps {
  data: IInfoBlock;
}
defineProps<IInfoBlockProps>();
</script>

<template>
  <div class="home-info-block-1">
    <div class="container">
      <div class="home-info-block-1__title">
        {{ data.title }}
      </div>
      <div class="home-info-block-1__body">
        <div class="home-info-block-1__img">
          <img src="./images/info-block-img.png" alt="info block 1" />
        </div>
        <div class="home-info-block-1__content">
          <p>
            RiskPools is a risk management protocol build on Ubix Network. It
            allows you to make money taking up risks, hedge risks, or minimize
            them. The key use cases for RiskPools are gaming, trading,
            insurance, forecasting, and betting.
            <br /><br />
            At the heart of the protocol are risk pools – a type of liquidity
            pools. Any random economic process associated with risk can serve as
            a base for a risk pool with its own token. You can supply liquidity
            to the pool, thus taking up the risk on yourself, or hedge your
            risk.
            <br /><br />
            If the risk event you are hedging against does materialize, you get
            a sum of money from the pool. But if the event doesn’t come to pass,
            the reward goes to the liquidity providers (pool holders).
            <br /><br />
            What makes RiskPools so powerful is that you can add data about a
            source of risk in a standardized way and create your own pools.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
