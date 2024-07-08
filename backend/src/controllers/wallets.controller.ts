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

const refund = async (req: Request, res: Response, next: NextFunction) => {
    try {
        const {fromAddress, signerPrivateKey} = req.body
        const instance = await initCilInstance(signerPrivateKey)
        const lastTransactions = await instance.queryApi('Address', fromAddress)
        const count = lastTransactions.recordsCount
        const lastPage = Math.floor(count/10)
        const { txInfoDTOs } = await instance.queryApi('Address', fromAddress, {page: lastPage})
        const targetTransaction = txInfoDTOs.pop()
        const transaction = await instance.createSendCoinsTx(
            [
                [
                    instance.stripAddressPrefix(fromAddress),
                    Number(targetTransaction.value)
                ]
            ],
            Number(process.env.CONCILIUM_ID) || 0
        );
        const fee = instance._estimateTxFee(
            transaction._data.payload.ins.length,
            transaction._data.payload.outs.length,
            true,
        );
        await instance.sendTx(transaction)
        res.status(200).json({
            hash: transaction.getHash(),
            sum: targetTransaction.value,
            fromAddress: CryptoWeb.keyPairFromPrivate(signerPrivateKey).address,
            toAddress: fromAddress,
            fee
        });
    } catch (e) {
        console.error(JSON.stringify(e))
        next(e);
    }
}

export default {
  create,
  balance,
  refund
};
