import { createApp } from "vue";
import { pinia, router } from "./providers";
import { VueQueryPlugin } from "vue-query";
import { Notify, Quasar } from "quasar";
import Vue3Sanitize from "vue-3-sanitize";
import VueApexCharts from "vue3-apexcharts";
import gtm from "@/shared/lib/plugins/gtm";

// Import icon libraries
import "@quasar/extras/material-icons/material-icons.css";
import "@quasar/extras/bootstrap-icons/bootstrap-icons.css"
// Import Quasar css
import "quasar/src/css/index.sass";

import App from "./App.vue";
import "aos/dist/aos.css";

import "./ui/scss/main.scss";

export const app = createApp(App)
  .use(pinia)
  .use(VueQueryPlugin)
  .use(router)
  .use(gtm)
  .use(VueApexCharts)
  .use(Vue3Sanitize, {
      allowedTags: ['p', 'span', 'div', 'br']
  })
  .use(Quasar, {
    plugins: {
      Notify,
    },
  });
