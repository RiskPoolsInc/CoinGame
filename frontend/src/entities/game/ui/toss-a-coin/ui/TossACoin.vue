<script setup lang="ts">
import BidCard from "@/features/game/bid-card";
import HashTable from "@/features/game/hash-table/ui/HashTable.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";

const columns = [
  {
    name: "Round",
    label: "Round",
    align: "left",
    field: "round",
  },
  {
    name: "result",
    align: "center",
    label: "Result",
    field: "result",
  },
];

const rows = [
  {
    result: "123123213123123123",
    round: 159,
  },
];
</script>

<template>
  <div class="toss-a-coin">
    <div class="container">
      <div class="toss-a-coin__title">112PxB_Start_game</div>

      <div class="toss-a-coin__subtitle">
        Place your bet and select the number of game rounds (coin tosses).
      </div>
      <div class="row">
        <div class="col-lg-8">
          <BidCard />
        </div>

        <div class="col-lg-4">
          <div class="toss-a-coin__subtitle">
            Place your bet and select the number of game rounds (coin tosses).
          </div>

          <HashTable :rows="rows" :columns="columns" />

          <div class="toss-a-coin__copy-btn text-right">
            <VButton
              label="COPY RESULT"
              outline
              color="dark"
              text-color="white"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
