const CIL_UTILS_API_URL = "https://explorer.ubikiri.com/api/"
const CIL_UTILS_RPC_PORT = 443
const CIL_UTILS_RPC_ADDRESS = "https://rpc-dv-1.ubikiri.com/"
const CIL_UTILS_RPC_USER = "cilTest"
const CIL_UTILS_RPC_PASS = "d49c1d2735536baa4de1cc6"
const BACKEND_URL = process.env.NODE_ENV === 'local' ? "http://localhost:3000/" : "https://api.coingame.dev20021.ubikiri.com/"
const MIN_BID = 10000
const MAX_BID = 10000000

export {
    CIL_UTILS_API_URL,
    CIL_UTILS_RPC_PORT,
    CIL_UTILS_RPC_ADDRESS,
    CIL_UTILS_RPC_USER,
    CIL_UTILS_RPC_PASS,
    BACKEND_URL,
    MIN_BID,
    MAX_BID
}