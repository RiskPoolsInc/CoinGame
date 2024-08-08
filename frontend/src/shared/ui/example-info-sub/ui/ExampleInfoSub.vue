<script setup lang="ts">
import ExampleInfoContent from "@/shared/ui/example-info-content/ui/ExampleInfoContent.vue";
</script>

<template>
  <div class="example-info-sub">
    <div class="container">
      <div class="row">
        <div class="col-lg-7 col-xs-12 example-info-sub__title">
          112x_How to participate in RiskPools
        </div>
      </div>
      <example-info-content title="As a player" btn-name="PLACE A BET" btn-link="/game">
        1) Top up the gaming wallet <br />
        You can deposit between 100,000 UBX (around $1) and 1,000,000,000 UBX. Buy UBX on Ubixpay, UBIX.Exchange, MEXC, BitMart, Uniswap, etc.
        <span>
          <a
              class="example-info-sub__context--link"
              href="https://coinmarketcap.com/currencies/ubix-network/#Markets"
          >
            UBIX Markets
          </a>
        </span>

        <br />
        <br />
        2) Decide how much you are willing to bid <br />
        In each game round, you can risk between 100,000 UBX and the total amount in your game wallet.
        <br />
        <br />
        3) Play the game!
        <br />
        <br />
        4) Receive the winnings<br />
        Winnings are added to your game wallet after each game, minus a 2% platform fee and the Ubix Network transaction fee (around 1,000 UBIX or $0.01). Processing the reward payout can take a few minutes, depending on network load.
        <br />
        <br />
        5) Withdraw the winnings<br />
        You can withdraw funds from your game wallet anytime after completing a game. The funds will be sent to the same wallet you used to deposit, with only the standard Ubix Network transaction fee. If you forget to withdraw, the funds will be returned automatically 24 hours later.
      </example-info-content>
      <example-info-content title="As a pool holder" btn-name="JOIN A RISK POOL" btn-link="/join-game">
        RiskPools isn’t just about playing games and hedging risks, it’s also a set of liquidity pools where risk is distributed among the pool holders. By joining a risk pool, you can earn passive income from games and other processes involving randomness and risk.
        <br/>
        <br/>
        We’ve prepared an article about liquidity pools and the risks of being a pool holder. Note: when you join a pool, you assume all responsibility for the consequences, including potential losses.
      </example-info-content>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
