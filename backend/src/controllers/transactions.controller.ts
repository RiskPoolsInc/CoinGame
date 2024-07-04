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

export default {
    completed
}
