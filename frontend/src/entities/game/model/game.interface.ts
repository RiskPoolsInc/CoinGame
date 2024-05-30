export interface IParityList {
  round: number;
  hashNumber: string;
  number: number;
  parity: boolean;
  currentBalance: number;
}

export interface IGameState {
  inProgress: boolean,
  wallet: string;
  balance: number;
  previousBalance: number;
  bid: number | null;
  bidForBalanceChart: number | null;
  round: number;
  parityList: IParityList[];
  uid: string;
  gameWalletAddress: string,
}
