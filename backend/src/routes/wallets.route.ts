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
/**
 * @swagger
 * /wallet/balance:
 *   get:
 *     summary: Get balance
 *     parameters:
 *       - in: query
 *         name: address
 *         schema:
 *           type: string
 *         required: true
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
 *                   balance:
 *                     type: number
 */
router.get("/balance", walletsController.balance);

export default router;
