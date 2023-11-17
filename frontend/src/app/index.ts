import { createApp } from "vue";
import { pinia, router } from "./providers";
import { VueQueryPlugin } from "vue-query";
import { Quasar } from "quasar";

// Import icon libraries
import "@quasar/extras/material-icons/material-icons.css";

// Import Quasar css
import "quasar/src/css/index.sass";

import App from "./App.vue";
import "aos/dist/aos.css";

import "./ui/scss/main.scss";

export const app = createApp(App)
  .use(pinia)
  .use(VueQueryPlugin)
  .use(router)
  .use(Quasar, {
    plugins: {},
  });
