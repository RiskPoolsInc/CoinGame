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
        You can deposit between 100,000 UBIX (around $1) and 1,000,000,000
        UBIX. You can buy UBIX on MEXC, BitMart, Uniswap etc.
        <span>
          <a
              class="example-info-sub__context--link"
              href="https://coinmarketcap.com/currencies/ubix-network/#Markets"
          >
            https://coinmarketcap.com/currencies/ubix-network/#Markets
          </a>
        </span>

        <br />
        <br />
        2) Decide how much you are willing to bid <br />
        In each game round, you can risk between 100,000 UBX and the total
        amount in the game wallet
        <br />
        <br />
        3) Play the game!
        <br />
        <br />
        4) Receive the winnings<br />
        Winnings will be added to your game wallet after each game, minus the
        2% platform fee and the Ubix Network transaction fee (around 1,000 UBX
        or $0.01). It can take up to a few minutes to process the reward
        payout, depending on the network load.
        <br />
        <br />
        5) Withdraw the winnings<br />
        You can withdraw the funds from the game wallet at any time after
        completing a game. The funds will be sent to the same wallet that you
        used to make the deposit, and the only fee you’ll pay is the standard
        Ubix Network transaction fee. If you forget to make a withdrawal, the
        funds will be sent back to you automatically 24 hours later.
      </example-info-content>
      <example-info-content title="As a pool holder" btn-name="JOIN A RISK POOL" btn-link="/join-game">
        RiskPools isn’t just about playing games and hedging one’s risks -  it’s also a set of liquidity pools within which the risk is distributed among the pool holders. By joining a risk pool, you’ll be able to earn a passive income from games and other processes that have an element of randomness and risk. <br><br>We’ve prepared an article about liquidity pools and the risks associated with being a pool holder. Note: when you join a pool, you assume all responsibility for the consequences, including potential losses.
      </example-info-content>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
