import CilUtils from 'cil-utils';

export default async (privateKey?: string) => {
    const utils = new CilUtils({
        privateKey: privateKey ||  'a'.repeat(64),
        apiUrl: process.env.CIL_UTILS_API_URL,
        rpcPort: process.env.CIL_UTILS_RPC_PORT,
        rpcAddress: process.env.CIL_UTILS_RPC_ADDRESS,
        rpcUser: process.env.CIL_UTILS_RPC_USER,
        rpcPass: process.env.CIL_UTILS_RPC_PASS,
    });
    await utils.asyncLoaded();

    return utils
}

