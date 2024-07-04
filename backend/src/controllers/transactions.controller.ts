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

const create =  async (req: Request, res: Response, next: NextFunction) => {
    const {signerPrivateKey, toAddress, sum} = req.body
    try {
        const instance = await initCilInstance(signerPrivateKey);
        const transaction = await instance.createSendCoinsTx(
            [
                [
                    instance.stripAddressPrefix(toAddress),
                    Number(sum)
                ]
            ],
            Number(process.env.CONCILIUM_ID) || 0
        );
        return {
            hash: transaction.getHash(),
            sum
        }
    } catch (e) {
        const err = e as Error
        if (err.message.includes('Not enough coins')) {
            res.status(402).json(err.message);
            return
        }
        next(e);
    }
}

export default {
    completed,
    create
}
