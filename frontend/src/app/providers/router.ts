import { createRouter, createWebHistory } from "vue-router";
import { routes } from "@/pages";

export const router = createRouter({
  history: createWebHistory("/"),
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition;
    } else if (to.hash) {
      return {
        el: to.hash,
        behavior: 'smooth',
      };
    } else {
      return { top: 0 };
    }
  },
  routes,
});
