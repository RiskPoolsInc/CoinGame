import {computed} from "vue";
import numberWithSpaces from "@/shared/lib/helpers/numberWithSpaces";
import {useGameStore} from "@/entities/game/model/game";

export const useGame = () => {
    const {gameState}  = useGameStore()

    const bidNotice = computed(() => 'Bid should be between ' + numberWithSpaces(process.env.VUE_APP_MIN_BID as string) + ' and ' + numberWithSpaces(process.env.VUE_APP_MAX_BID as string) + ' UBX')

    const formattedBalance = computed(() => numberWithSpaces(gameState.balance))

    return {
        bidNotice,
        formattedBalance
    }
}
