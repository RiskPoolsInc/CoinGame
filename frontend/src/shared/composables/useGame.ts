import {computed} from "vue";
import {MIN_BID, MAX_BID} from "../../../config";
import numberWithSpaces from "@/shared/lib/helpers/numberWithSpaces";
import {useGameStore} from "@/entities/game/model/game";

export const useGame = () => {
    const {gameState}  = useGameStore()

    const bidNotice = computed(() => 'Bid should be between ' + numberWithSpaces(MIN_BID) + ' and ' + numberWithSpaces(MAX_BID) + ' UBX')

    const formattedBalance = computed(() => numberWithSpaces(gameState.balance))

    return {
        bidNotice,
        formattedBalance
    }
}
