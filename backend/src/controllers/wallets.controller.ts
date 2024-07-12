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
        const {signerPrivateKey} = req.body
        const signerAddress = CryptoWeb.keyPairFromPrivate(signerPrivateKey).address
        const instance = await initCilInstance(signerPrivateKey)
        const lastTransactions = await instance.queryApi('Address', signerAddress)
        const count = lastTransactions.recordsCount
        const lastPage = Math.floor(count/10)
        const { txInfoDTOs } = await instance.queryApi('Address', signerAddress, {page: lastPage})
        const targetTransaction = txInfoDTOs.pop()
        const originAddress = targetTransaction.inputs[0].from
        const transaction = await instance.createSendCoinsTx(
            [
                [
                    instance.stripAddressPrefix(originAddress),
                    -1
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
            sum: transaction.value,
            fromAddress: signerAddress,
            toAddress: originAddress,
            fee
        });
    } catch (e) {
        console.error(JSON.stringify(e))
        const err = e as ResponseError;
        if (err.message.includes('Not enough coins')) {
            res.status(402).json(err.message);
            return
        }
        next(e);
    }
}

export default {
  create,
  balance,
  refund
};
