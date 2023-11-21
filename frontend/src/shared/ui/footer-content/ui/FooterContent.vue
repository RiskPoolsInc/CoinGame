<script setup lang="ts"></script>

<template>
  <div class="footer-content">
    <div class="container">
      <div class="footer-content__logo">
        <img src="./images/logo.svg" alt="footer-content logo" />
      </div>

      <div class="footer-content__main row justify-between">
        <div class="footer-content__company-label col-lg-2 col-xs-12">
          (с) Riskpools 2023
        </div>
        <div class="footer-content__links row col-lg-4 col-xs-12">
          <router-link class="footer-content__link" to="/docuemts">
            Documents
          </router-link>

          <router-link class="footer-content__link" to="/doc">
            Terms of use
          </router-link>

          <router-link class="footer-content__link" to="/privacy">
            Privacy
          </router-link>
        </div>
        <div></div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
