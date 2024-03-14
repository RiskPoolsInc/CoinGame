export interface IParityList {
  round: number;
  hashNumber: string;
  number: number;
  parity: boolean;
  currentBalance: number;
}

export interface IGameState {
  wallet: string;
  balance: number;
  previousBalance: number;
  bid: number | null;
  bidForBalanceChart: number | null;
  round: number;
  parityList: IParityList[];
  gameWalletKeyPair: any;
  transitWalletKeyPair: any;
  poolWalletKeyPair: any;
  profitWalletKeyPair: any;
  projectWalletKeyPair: any;
  gameWalletCilUtils: any;
  transitWalletCilUtils: any;
  poolWalletCilUtils: any;
  profitWalletCilUtils: any;
  projectWalletCilUtils: any;
}
