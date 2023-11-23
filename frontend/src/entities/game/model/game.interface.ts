export interface IHashNumber {
  round: number;
  hashNumber: string;
}

export interface IParityList {
  round: number;
  number: number;
  parity: boolean;
}

export interface IGameState {
  wallet: string;
  balance: number;
  bid: number;
  round: number;
  hashNumberList: IHashNumber[];
  parityList: IParityList[];
}
