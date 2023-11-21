import type { RouteRecordRaw } from "vue-router";

export const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: () => import("./main/ui/MainPage.vue"),
  },
  {
    path: "/base-components",
    name: "components",
    component: () => import("./base-components/ui/BaseComponentsPage.vue"),
  },
  {
    path: "/example",
    name: "example-page",
    component: () => import("./example"),
  },
];
