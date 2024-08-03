import { Request, Response, NextFunction} from "express";

export default (req: Request, res: Response, next: NextFunction) => {
    const origin = req.get('Origin') || req.get('Referer');

    if (process.env.ALLOWED_ORIGIN && origin && origin.startsWith(process.env.ALLOWED_ORIGIN)) {
        next();
    } else {
        res.status(403).send('Access denied');
    }
}
