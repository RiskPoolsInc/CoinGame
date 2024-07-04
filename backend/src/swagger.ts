import swaggerJsDoc from 'swagger-jsdoc';
import { Express } from "express";
import swaggerUI from 'swagger-ui-express'

const swaggerOptions = {
    swaggerDefinition: {
        openapi: '3.0.0',
        info: {
            title: 'API Documentation',
            version: '1.0.0',
            description: 'API Information',
            contact: {
                name: 'Developer',
            },
            servers: [{ url: 'http://localhost:3000' }],
        },
    },
    apis: ['./src/routes/*.ts'],
};

const swaggerDocs = swaggerJsDoc(swaggerOptions);

export default (app: Express) => {
    app.use('/api-docs', swaggerUI.serve, swaggerUI.setup(swaggerDocs));
};

