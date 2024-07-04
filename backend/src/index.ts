import express, { Express, Request, Response } from "express";
import dotenv from "dotenv";
import cors from "cors";
import walletsRoute from "./routes/wallets.route";
import swaggerSetup from "./swagger";

dotenv.config();

const app: Express = express();
const port = process.env.PORT || 3000;

swaggerSetup(app)
app.use(cors());
app.get("/", (req: Request, res: Response) => {
  res.send("Express + TypeScript Server");
});
app.use("/wallet", walletsRoute);

app.use((err: ResponseError, req: Request, res: Response) => {
  const statusCode = err?.status || 500;
  console.error(err.message, err.stack);
  res.status(statusCode).json({'message': err.message});

  return;
});

app.listen(port, () => {
  console.log(`[server]: Server is running at http://localhost:${port}`);
});
