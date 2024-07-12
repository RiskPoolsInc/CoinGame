import {NextFunction, Request, Response} from "express";
import initCilInstance from "../utils/initCilInstance";
import {errorResponseMap} from "../consts";

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
        console.log(e)
        const err = e as ResponseError
        for (const [message, statusCode] of Object.entries(errorResponseMap)) {
            if (err.message.includes(message)) {
                res.status(statusCode).json(err.message);
                return;
            }
        }
        next(e);
    }
}

export default {
    completed,
    send
}
