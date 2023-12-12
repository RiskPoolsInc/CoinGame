<script setup lang="ts">
import BidCard from "@/features/game/bid-card";
import HashTable from "@/features/game/hash-table/ui/HashTable.vue";
import VButton from "@/shared/ui/base-components/v-button/ui/VButton.vue";
import { useGameStore } from "@/entities/game/model/game";
import { HASH_TABLE_COLUMNS } from "@/entities/game/model/constants";

const { gameState } = useGameStore();

const downloadTxtFile = () => {
  let text = "";

  text = HASH_TABLE_COLUMNS.map((item) => item.label).join("  ") + "\n" + text;

  text =
    text +
    gameState.parityList
      .map((item, index) => {
        return index + 1 + " " + item.hashNumber;
      })
      .join("    \n") +
    "\n";

  const element = document.createElement("a");
  const file = new Blob([text], { type: "text/plain" });
  element.href = URL.createObjectURL(file);
  element.download = "hash.txt";
  document.body.appendChild(element); // Required for this to work in FireFox
  element.click();
};
</script>

<template>
  <div class="toss-a-coin">
    <div class="toss-a-coin__title">112PxB_Start_game</div>

    <div class="row justify-between">
      <div class="col-lg-7">
        <div class="toss-a-coin__subtitle">
          Place your bet and select the number of game rounds
          <br class="xl-hide lg-hide md-hide" />
          (coin tosses).
        </div>
      </div>

      <div class="col-lg-4 sm-hide xs-hide md-hide">
        <div class="toss-a-coin__subtitle">
          Coin toss results as hashes of randomly generated numbers.
        </div>
      </div>

      <div class="col-lg-7 col-xs-12">
        <BidCard />
      </div>

      <div class="col-lg-4 col-sm-8 xl-hide lg-hide">
        <div class="toss-a-coin__subtitle">
          Coin toss results as hashes of randomly generated numbers.
        </div>
      </div>

      <div class="col-lg-4 col-sm-8 col-xs-12 toss-a-coin__hash-table">
        <HashTable
          id="hash-table"
          :rows="gameState.parityList"
          :columns="HASH_TABLE_COLUMNS"
        />

        <div class="toss-a-coin__copy-btn text-right">
          <VButton
            label="COPY RESULT"
            outline
            color="dark"
            text-color="white"
            size="lg"
            @click="downloadTxtFile"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
