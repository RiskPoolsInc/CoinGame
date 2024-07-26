export interface IParityList {
  round: number;
  hashNumber: string;
  number: number;
  parity: boolean;
  currentGameRoundSum: number
}

export interface IGameState {
  inProgress: boolean,
  wallet: string;
  balance: number;
  previousBalance: number;
  round: number;
  uid: string;
  gameWalletAddress: string,
  isPrepared: boolean
}
