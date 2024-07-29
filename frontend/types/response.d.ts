declare type ResponseGameRound = {
    createdOn: string
    id: string
    number: number
    result: {
        id: number
        code: 'Win' | 'Lose'
    }
    roundNumber: number
    currentGameRoundSum: number
}