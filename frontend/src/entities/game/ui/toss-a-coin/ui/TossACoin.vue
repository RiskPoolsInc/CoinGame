<script setup lang="ts">
import BidCard from "@/features/game/bid-card";
import HashTable from "@/features/game/hash-table/ui/HashTable.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";
import { HASH_TABLE_COLUMNS } from "@/entities/game/model/constants";

const { gameState } = useGameStore();
</script>

<template>
  <div class="toss-a-coin">
    <div class="container">
      <div class="toss-a-coin__title">112PxB_Start_game</div>

      <div class="row justify-between">
        <div class="col-lg-7">
          <div class="toss-a-coin__subtitle">
            Place your bet and select the number of game rounds (coin tosses).
          </div>
        </div>

        <div class="col-lg-4">
          <div class="toss-a-coin__subtitle">
            Coin toss results as hashes of randomly generated numbers.
          </div>
        </div>
        <div class="col-lg-7">
          <BidCard />
        </div>
        <div class="col-lg-4">
          <HashTable
            :rows="gameState.parityList"
            :columns="HASH_TABLE_COLUMNS"
          />

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
