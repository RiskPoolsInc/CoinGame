<script setup lang="ts">
import CalculateHash from "@/features/game/calculate-hash";
</script>

<template>
  <div class="check-game-page">
    <div class="container">
      <div class="container check-game-page__wrapper">
        <div class="check-game-page__title">
          112PxD_How do we ensure transparency?
        </div>

        <div class="check-game-page__content">
          <p>
            We can’t physically toss a coin on the blockchain, of course. Instead, we use provable randomness: an algorithm that generates random numbers between 1 and 1,000,000, replacing heads and tails with even and odd numbers. A SHA256 function calculates the hash of each number.
            <br />
            <br />
            At the start of each game, we show the player the hashes of all the numbers (as many hashes as there are rounds in the game). At the end of the game, the player can view the numbers themselves and verify that their hashes match those shown at the start.
            <br />
            <br />
            We’ve even prepared a small program that calculates the hash of any number. You can use it to verify the game results. You can find the program in the <a href="https://github.com/RiskPoolsInc/rng-library" target="_blank">public repository</a> or use it directly here.
          </p>
        </div>

        <img
          class="check-game-page__bg"
          src="./images/bg.png"
          alt="check-game bg"
        />

        <CalculateHash />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
