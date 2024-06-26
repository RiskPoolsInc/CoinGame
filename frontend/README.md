# frontend

## Config
You need to fill out (if necessary create) the .env file in the project root (/frontend folder):\
```
VUE_APP_BACKEND_URL=

VUE_APP_MIN_BID=10000
VUE_APP_MAX_BID=10000000
```

VUE_APP_BACKEND_URL — backend URL in the format https://site.com/.

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
