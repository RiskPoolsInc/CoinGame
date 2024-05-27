# frontend

## Config
You need to fill out (if necessary create) the config.js file in the project root (/frontend folder):\
```
const CIL_UTILS_RPC_ADDRESS = ""
const CIL_UTILS_RPC_USER = ""
const CIL_UTILS_RPC_PASS = ""
const BACKEND_URL=""
constant MIN_BID = 10000
constant MAX_BID = 10000000
```

The variables CIL_UTILS_RPC_ADDRESS, CIL_UTILS_RPC_USER, CIL_UTILS_RPC_PASS are needed to interact with the library running on the network. Find out the values from the developer of this library. BACKEND_URL — backend URL in the format https://site.com/.

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).

### Project structure
```
/Shared - Reusable functionality, detached from the specifics of the project/business. (e.g. UIKit, libs, API)
