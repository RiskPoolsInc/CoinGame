<script setup lang="ts"></script>

<template>
  <div class="example-info-block">
    <div class="container">
      <div class="row">
        <div class="col-lg-8 example-info-block__title">
          111x_RiskPools protocol in action: a coin flip game
        </div>
      </div>

      <div class="example-info-block__content row">
        <div class="col-lg-8">
          <p class="example-info-block__context">
            In this classic children’s game, one player, let’s call him Bob, tosses a coin and covers it with his hand. The other player, Alice, has to guess if it’s heads or tails. In our scenario, Alice plays against a risk pool. If she guesses correctly, she’ll receive the reward from the pool; if she guesses incorrectly, the reward goes to the pool holder.
            <br />
            <br />
            Now, let’s toss the coin several times instead of once. Alice’s reward balance grows when she guesses correctly and decreases when she’s wrong. If the balance reaches zero, the game stops; if after a specified number of rounds it’s still above zero, she receives the winnings.
            <br />
            <br />
            To make things more interesting and level the playing field, we introduce the following conditions:
            <br />
            <br />
            1) The player starts by bidding a certain amount.
            <br />
            2) Heads (even number): the player wins a percentage of the bid amount.
            <br />
            3) Tails (odd number): the player loses a percentage of the bid amount.
            <br />
            4) The game can have 3 to 10 rounds, depending on the player’s preferences.
          </p>
        </div>
        <div class="col-lg-4">
          <img class="example-info-block__img" src="./images/hand.png" alt="" />
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
