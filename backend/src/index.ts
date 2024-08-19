import express, {Express, Request, Response, NextFunction} from "express";
import dotenv from "dotenv";
import cors from "cors";
import bodyParser from 'body-parser';
import walletsRoute from "./routes/wallets.route";
import transactionsRoute from "./routes/transactions.route";
import swaggerSetup from "./swagger";
import apiKeyMiddleware from "./utils/apiKeyMiddleware";
import originMiddleware from "./utils/originMiddleware";

dotenv.config();

const app: Express = express();
const port = process.env.PORT || 3000;

swaggerSetup(app)
app.use(cors());
app.use(apiKeyMiddleware);
//app.use(originMiddleware);
app.use(bodyParser.json({ limit: '500kb' }));

app.get("/", (req: Request, res: Response) => {
  res.send("Express + TypeScript Server");
});
app.use("/wallet", walletsRoute);
app.use("/transactions", transactionsRoute);


app.listen(port, () => {
  console.log(`[server]: Server is running at http://localhost:${port}`);
});
