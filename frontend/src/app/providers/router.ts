import { createRouter, createWebHistory } from "vue-router";
import { routes } from "@/pages";

export const router = createRouter({
  history: createWebHistory("/"),
  scrollBehavior() {
    // always scroll to top
    return { top: 0 };
  },
  routes,
});
