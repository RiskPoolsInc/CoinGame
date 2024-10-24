# backend

## Config
You need to fill out (if necessary create) the .env file in the project root (/backend folder):
```
PORT=
CIL_UTILS_API_URL=
CIL_UTILS_RPC_PORT=
CIL_UTILS_RPC_ADDRESS=
CIL_UTILS_RPC_USER=
CIL_UTILS_RPC_PASS=
CONCILIUM_ID=
INVOKE_FEE=
API_KEY=
ALLOWED_ORIGIN=
```

PORT — Backend port.
CIL_UTILS_API_URL — Explorer API URL in the format https://site.com/.
CIL_UTILS_RPC_PORT — An integer of RPC port.
CIL_UTILS_RPC_USER — RPC URL in the format https://site.com/.
CIL_UTILS_RPC_USER — RPC user.
CIL_UTILS_RPC_PASS — RPC password.
CONCILIUM_ID — An integer of concilium id.
INVOKE_FEE — An integer of contract invoke fee.
API_KEY — A string of api key.
ALLOWED_ORIGIN — Origin URL in the format https://site.com/.

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run dev
```

### Compiles and minifies for production
```
npm run start
```

### Lints and fixes files
```
npm run lint:fix
npm run lint:check
npm run format:check
npm run format:write
```
