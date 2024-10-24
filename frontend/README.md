# frontend

## Config
You need to fill out (if necessary create) the .env file in the project root (/frontend folder):
```
VUE_APP_BACKEND_URL=

VUE_APP_MIN_BID=10000
VUE_APP_MAX_BID=10000000

VUE_APP_CIL_UTILS_API_URL=https://test-explorer.ubikiri.com/api/
VUE_APP_CIL_UTILS_RPC_PORT=443
VUE_APP_CIL_UTILS_RPC_ADDRESS=https://rpc-dv-1.ubikiri.com/
VUE_APP_CIL_UTILS_RPC_USER=
VUE_APP_CIL_UTILS_RPC_PASS=

VUE_APP_GOOGLE_TAG_MANAGER_ID=
```

VUE_APP_BACKEND_URL — Backend URL in the format https://site.com/.
VUE_APP_MIN_BID — An integer of minimum bid.
VUE_APP_MAX_BID — An integer of maximum bid.
VUE_APP_CIL_UTILS_API_URL — Explorer API URL in the format https://site.com/.
VUE_APP_CIL_UTILS_RPC_PORT — An integer of RPC port.
VUE_APP_CIL_UTILS_RPC_ADDRESS — RPC URL in the format https://site.com/.
VUE_APP_CIL_UTILS_RPC_USER — RPC user.
VUE_APP_CIL_UTILS_RPC_PASS — RPC password.
VUE_APP_GOOGLE_TAG_MANAGER_ID — Google tag manager project ID corresponding to this application.

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
/Pages - Full pages or large parts of a page in nested routing.
/Widgets - large self-contained chunks of functionality or UI, usually delivering an entire use case.
/Features - reused implementations of entire product features, i.e. actions that bring business value to the user.
/App - everything that makes the app run — routing, entrypoints, global styles, providers.
/Entities - business entities that the project works with, like user or product.

```
