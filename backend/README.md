
# RiskPools backend

  

## Setup

Install all the packages:

```npm install```

  

Edit all the environment variables in .env file. You can ask devops for the necessary credentials.

  

Run the script with:

```node app.js```

If started correctly, you'll see: `RiskPool backend listening on port ...`

  

## Environment variables

You need to set up variables in .env file:

|variable|description  |
|--|--|
|REFUNDS_DB_PATH|path where leveldb instance with refunds will be stored  |
|OPERATIONS_LOG_DB_PATH  | path where leveldb instance with played games will be stored |
|CIL_UTILS_API_URL| credentials for Cil Utils |
|CIL_UTILS_RPC_PORT| credentials for Cil Utils |
|CIL_UTILS_RPC_ADDRESS| credentials for Cil Utils |
|CIL_UTILS_RPC_USER| credentials for Cil Utils |
|CIL_UTILS_RPC_PASS| credentials for Cil Utils |
|POOL_WALLET_ADDRESS| Адрес кошелька Риск пула |
|POOL_WALLET_PRIVATE_KEY| Приватный ключ кошелька Риск пула |
|POOL_WALLET_PUBLIC_KEY| Публичный ключ кошелька Риск пула |
|PROJECT_WALLET_ADDRESS| Адрес кошелька прибыли организатора |
|PROFIT_WALLET_ADDRESS| Адрес кошелька Выручки проекта в Юбистейк |
|CORS_ORIGIN| URL of frontend |
|MIN_BID| Minimum allowed bid |
|MAX_BID| Maximum allowed bid |

  

## Endpoints

Swagger documentation is available at `/api-docs` URL when the server runs

  

## Logs

For currently running operations, you will see some output in the console:

```Play game with wallet: 21f1622506fa032038e72b5abd7153dd4d9242b0```

or

```Uploading wallet: 21f1622506fa032038e72b5abd7153dd4d9242b0```

  

There're 2 leveldb storages, `operations_log` for the log of all games played with transit and game wallet key pairs saved, and `refunds` for non-performed refunds. All the performed refunds are removed from the DB.

  

## Logic of work

Currently, endpoint supports:

  

- Uploading a game wallet. For each uploaded wallet, in 24h (from starting the backend or adding the wallet to DB - which comes later) all the funds will be refunded to the first wallet from which funds were sent to this wallet.

  

- Playing a game. All the logic of the game is performed at backend, frontend only receives the results.
