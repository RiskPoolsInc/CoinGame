declare module 'crypto-web'

interface ResponseError extends Error {
    status?: number;
}
