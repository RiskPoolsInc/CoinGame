<script setup lang="ts">
import CalculateHash from "@/features/game/calculate-hash";
</script>

<template>
  <div class="check-game-page">
    <div class="container check-game-page__wrapper">
      <div class="check-game-page__title">
        112PxD_How do we ensure transparency and randomness?
      </div>

      <div class="check-game-page__content">
        <p>
          We can’t physically toss a coin on blockchain, of course. Instead, we
          use provable randomness: an algorithm that generates random numbers
          between 1 and 1,000,000, and we replace heads and tails with even and
          odd numbers. <br />
          A SHA256 function is used to calculate the hash of each number.
          <br />
          <br />
          At the start of each game, we show the player the hashes of all the
          numbers (as many hashes as there are rounds in the game). At the end
          of the game, the player can view the numbers themselves and verify
          that their hashes are the same as those shown at the start.
          <br />
          <br />
          We’ve even prepared a small program that will calculate the hash of
          any number. You can use it to verify the result of a game. You can
          find the program in the public repository (link) or use it directly
          here.
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
</template>

<style scoped lang="scss">
@import "styles.module";
</style>
