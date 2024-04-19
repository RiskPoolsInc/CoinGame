# RiskPools backend

## Setup
Install all the packages:
```npm install```

Edit all the environment variables in .env file. You can ask devops for the necessary credentials.

Run the script with:
```node app.js```
If started correctly, you'll see: `RiskPool backend listening on port ...`

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