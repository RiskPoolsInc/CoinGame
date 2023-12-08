<script setup lang="ts">
const scrollToTop = () => {
  window.scrollTo({
    top: 0,
    behavior: "smooth",
  });
};
</script>

<template>
  <div class="footer-content">
    <div class="container">
      <div class="footer-content__main row justify-between">
        <div class="col-lg-2 col-md-2">
          <div class="footer-content__logo">
            <img
              @click="scrollToTop"
              src="./images/logo.svg"
              alt="footer-content logo"
            />
          </div>
        </div>

        <div
          class="footer-content__links row col-lg-8 col-md-8 col-xs-12 justify-center"
        >
          <router-link class="footer-content__link" to="/docuemts">
            Our documents
          </router-link>

          <router-link class="footer-content__link" to="/doc">
            Terms of use
          </router-link>

          <router-link class="footer-content__link" to="/privacy">
            Privacy Policy
          </router-link>
        </div>

        <div
          class="footer-content__company-label col-lg-2 col-md-2 col-xs-12 text-right"
        >
          (с) Riskpools 2023
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
