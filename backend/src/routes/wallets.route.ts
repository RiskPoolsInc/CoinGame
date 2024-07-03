import express from "express";
import walletsController from "../controllers/wallets.controller";
const router = express.Router();

router.put("/create", walletsController.create);

export default router;
