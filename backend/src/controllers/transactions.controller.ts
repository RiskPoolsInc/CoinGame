import {NextFunction, Request, Response} from "express";
import initCilInstance from "../utils/initCilInstance";

const completed =  async (req: Request, res: Response, next: NextFunction) => {
    const hash = req.query.hash
    try {
        const instance = await initCilInstance();
        await instance.queryApi('Transaction', hash);
        res.status(200).json({
            hash,
            IsCompleted: true
        });
    } catch (e) {
        console.error(JSON.stringify(e))
        const err = e as {response: ResponseError}
        if (err.response.status === 404) {
            res.status(200).json({
                hash,
                IsCompleted: false
            });
            return
        }
        next(e);
    }
}

const send =  async (req: Request, res: Response, next: NextFunction) => {
    const {signerPrivateKey, receivers} = req.body
    try {
        const sum = receivers
            .map((receiver: Receiver) => receiver[1])
            .reduce((accumulator: number, currentValue: number) => accumulator + currentValue ,0);
        if (sum <= 0) {
            res.status(402).json('Not enough coins');
        }

        const instance = await initCilInstance(signerPrivateKey);
        const transaction = await instance.createSendCoinsTx(
            receivers.map((receiver: Receiver) => ([
                instance.stripAddressPrefix(receiver[0]),
                receiver[1]
            ])),
            Number(process.env.CONCILIUM_ID) || 0
        );
        await instance.sendTx(transaction)

        res.status(200).json({
            hash: transaction.getHash(),
            sum
        });
    } catch (e) {
        const err = e as ResponseError
        if (err.message.includes('Not enough coins')) {
            res.status(402).json(err.message);
            return
        }
        next(e);
    }
}

export default {
    completed,
    send
}
