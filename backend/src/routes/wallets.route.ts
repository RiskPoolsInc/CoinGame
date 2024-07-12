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

/**
 * @swagger
 * /wallet/refund:
 *   post:
     *     summary: Refund
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - signerPrivateKey
 *             properties:
 *               signerPrivateKey:
 *                 type: string
 *     responses:
 *       200:
 *         description: Success
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 hash:
 *                   type: string
 *                 sum:
 *                   type: number
 *                 fromAddress:
 *                   type: string
 *                 toAddress:
 *                   type: string
 *                 fee:
 *                   type: number
 */
router.post("/refund", walletsController.refund);

export default router;
