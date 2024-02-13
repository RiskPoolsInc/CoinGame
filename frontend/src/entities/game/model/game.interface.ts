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
  bid: number;
  round: number;
  parityList: IParityList[];
  gameWalletKeyPair: any;
  transitWalletKeyPair: any;
  gameWalletCilUtils: any;
  transitWalletCilUtils: any;
}
