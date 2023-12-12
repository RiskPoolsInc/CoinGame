import { IInfoBlock } from "@/shared/ui/home-info-block-1/model/InfoBLock.interface";

export const BLOCKS: Array<IInfoBlock> = [
  {
    title: "1x_How does RiskPools work?",
    description:
      "RiskPools is a risk management protocol build on Ubix Network. It allows you to make money taking up risks, hedge risks, or minimize them. The key use cases for RiskPools are gaming, trading, insurance, forecasting, and betting. <br> At the heart of the protocol are risk pools – a type of liquidity pools. Any random  economic process associated with  risk can serve as a base for a risk pool with its own token. You can supply liquidity to the pool, thus taking up the risk on yourself, or hedge your risk. <br> If the risk event you are hedging against does materialize, you get a sum of money from the pool. But if the event doesn’t come to pass, the reward goes to the liquidity providers (pool holders). <br> What makes RiskPools so powerful is that you can add data about      a source of risk in a standardized way and create your own pools.",
    position: "center",
    image: "https://picsum.photos/seed/1/400/300",
  },
  {
    title:
      "11x_A basic example: <br class='md-hide lg-hide xl-hide sm-hide'> Flip a coin",
    description:
      "RiskPools is a risk management protocol build on Ubix Network. It allows you to make money taking up risks, hedge risks, or minimize them. The key use cases for RiskPools are gaming, trading, insurance, forecasting, and betting. <br> At the heart of the protocol are risk pools – a type of liquidity pools. Any random  economic process associated with  risk can serve as a base for a risk pool with its own token. You can supply liquidity to the pool, thus taking up the risk on yourself, or hedge your risk. <br> If the risk event you are hedging against does materialize, you get a sum of money from the pool. But if the event doesn’t come to pass, the reward goes to the liquidity providers (pool holders). <br> What makes RiskPools so powerful is that you can add data about      a source of risk in a standardized way and create your own pools.",
    position: "center",
    image: "https://picsum.photos/seed/1/400/300",
  },
  {
    title: "2x_Technology",
    description:
      "Protocol revenue is distributed as follows: if a player loses, 20% of their bid is deposited in the risk pool to boost token backing. The remaining 80% is distributed among the pool’s stakers via Ubistake.io.<br> <br>Risk pools will use smart contracts residing in a separate consilium (essentially our own blockchain) built on the Ubix.Network DAG. This is a very flexible technology: our only limitation is the shared address space. <br><br>Each pool will have its own token (similar to LP tokens on a DEX), issued using Ubix.Network’s native T-10 standard. This is necessary to record each pool holder’s share and the corresponding share of the revenue. Pool profits will be distributed using Ubstake.io. <br><br>We will balance the pools using probability fields and non-ergodic random processes. The notion of a random value or process will be formulated individually for each pool. All related documents will be posted on GitHub.",
    position: "center",
    image: "https://picsum.photos/seed/1/400/300",
  },
];
