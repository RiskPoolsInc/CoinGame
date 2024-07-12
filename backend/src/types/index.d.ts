declare module 'crypto-web'
declare module 'cil-utils'

interface ResponseError extends Error {
    status?: number;
    code?: number
}
