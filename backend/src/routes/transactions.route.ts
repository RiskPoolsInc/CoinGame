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

export default router;
