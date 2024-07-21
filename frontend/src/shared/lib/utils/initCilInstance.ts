import CilUtils from 'cil-utils';

export default async () => {
    const utils = new CilUtils({
        privateKey: 'a'.repeat(64),
        apiUrl: process.env.VUE_APP_CIL_UTILS_API_URL,
        rpcPort: process.env.VUE_APP_CIL_UTILS_RPC_PORT,
        rpcAddress: process.env.VUE_APP_CIL_UTILS_RPC_ADDRESS,
        rpcUser: process.env.VUE_APP_CIL_UTILS_RPC_USER,
        rpcPass: process.env.VUE_APP_CIL_UTILS_RPC_PASS,
    });
    await utils.asyncLoaded();

    return utils
}

