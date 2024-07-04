import express from "express";
import walletsController from "../controllers/wallets.controller";
const router = express.Router();

/**
 * @swagger
 * /wallet/create:
 *   put:
 *     summary: Create wallet
 *     responses:
 *       200:
 *         description: Success
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                   address:
 *                     type: string
 *                   privateKey:
 *                     type: string
 */
router.put("/create", walletsController.create);
//router.get("/create", walletsController.balance);

export default router;
