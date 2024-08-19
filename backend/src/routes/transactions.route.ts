import express from "express";
import transactionsController from "../controllers/transactions.controller";
const router = express.Router();


/**
 * @swagger
 * /transactions/completed:
 *   get:
 *     summary: Check transaction status
 *     parameters:
 *       - in: query
 *         name: hash
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
 *                   hash:
 *                     type: string
 *                   IsCompleted:
 *                     type: boolean
 */
router.get("/completed", transactionsController.completed);
/**
 * @swagger
 * /transactions/send:
 *   post:
 *     summary: Create and transaction
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - signerPrivateKey
 *               - receivers
 *             properties:
 *               signerPrivateKey:
 *                 type: string
 *               receivers:
 *                 type: array
 *                 items:
 *                   type: object
 *                   properties:
 *                    address:
 *                     type: string
 *                    sum:
 *                      type: number
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
 *                   type: string
 *                 fee:
 *                   type: string
 */

router.post("/send", transactionsController.send);

/**
 * @swagger
 * /transactions/fee:
 *   post:
 *     summary: Calc fee
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - signerPrivateKey
 *               - receivers
 *             properties:
 *               signerPrivateKey:
 *                 type: string
 *               receivers:
 *                 type: array
 *                 items:
 *                   type: object
 *                   properties:
 *                    address:
 *                     type: string
 *                    sum:
 *                      type: number
 *     responses:
 *       200:
 *         description: Success
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 fee:
 *                   type: string
 */
router.post("/fee", transactionsController.calcFee);

export default router;
