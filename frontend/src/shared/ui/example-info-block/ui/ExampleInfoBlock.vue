<script setup lang="ts"></script>

<template>
  <div class="example-info-block">
    <div class="container">
      <div class="row">
        <div class="col-lg-7 example-info-block__title">
          111x_RiskPools protocol in action: a coin flip game
        </div>
      </div>

      <div class="example-info-block__content row">
        <div class="col-lg-8">
          <p class="example-info-block__context">
            In this classic children’s game, one player – call him Bob - tosses
            a coin and covers it with their hand. The other player – Alice - has
            to guess if it’s heads or tails. In our scenario, Alice plays
            against a risk pool. If she guesses right, she’ll get the reward
            from the pool; but if she guesses wrong, the reward will go to the
            pool holder.
            <br />
            <br />
            Now let’s toss the coin several times instead of once. Alice’s
            reward balance grows whenever she guesses correctly and goes down
            when she is wrong. If the balance reaches zero, the game stops; and
            if after a specified number of rounds it’s still more than zero, she
            receives the winnings.
            <br />
            <br />
            To make things even more interesting and level the playing field,
            let’s introduce the following conditions:
            <br />
            <br />
            1) The player starts by bidding a certain amount.
            <br />
            2) Heads (even number): the player wins a % of the bid's amount.
            <br />
            3) Tails (odd number): the player loses a % of the bid's amount.
            <br />
            4) The game can have 3 to 10 rounds, depending on the player’s
            preferences.
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
