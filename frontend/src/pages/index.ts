import type { RouteRecordRaw } from "vue-router";

export const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: () => import("./main/ui/MainPage.vue"),
  },
  {
    path: "/example",
    name: "example-page",
    component: () => import("./example/ui/ExamplePage.vue"),
  },
  {
    path: "/game",
    name: "game-page",
    component: () => import("./game"),
  },
  {
    path: "/join-game",
    name: "join-game-page",
    component: () => import("./join-game-page"),
  },
  {
    path: "/check-game",
    name: "check-game-page",
    component: () => import("./check-game"),
  },
];
