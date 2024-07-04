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
 * /transactions/create:
 *   post:
 *     summary: Create transaction
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             type: object
 *             required:
 *               - signerPrivateKey
 *               - toAddress
 *               - sum
 *             properties:
 *               signerPrivateKey:
 *                 type: string
 *               toAddress:
 *                 type: string
 *               sum:
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
 *                   type: string
 */

router.post("/create", transactionsController.create);

export default router;
