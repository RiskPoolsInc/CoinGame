import {NextFunction, Request, Response} from "express";
import CryptoWeb from 'crypto-web'
import initCilInstance from "../utils/initCilInstance";
const create = async (req: Request, res: Response, next: NextFunction) => {
    try {
      const kpOwner = CryptoWeb.createKeyPair();
      res.status(200).json({
          address: kpOwner.address,
          privateKey: kpOwner.privateKey
      });
    } catch (e) {
      console.error(JSON.stringify(e))
      next(e);
    }
};

const balance = async (req: Request, res: Response, next: NextFunction) => {
    try {
        const address = req.query.address
        const instance = await initCilInstance()
        const balance = await instance.getBalance(address)
        res.status(200).json({
            address,
            balance
        });

    } catch (e) {
        console.error(JSON.stringify(e))
        next(e);
    }
}

export default {
  create,
  balance
};
