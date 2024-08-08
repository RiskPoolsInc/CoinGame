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
        <div class="col-lg-2 col-md-2 sm-2">
          <div class="footer-content__logo">
            <img @click="scrollToTop" src="./images/logo.svg" alt="footer-content logo" />
          </div>
        </div>

        <div class="footer-content__links row col-lg-8 col-md-8 col-sm-10 col-xs-12 justify-center">
          <!-- <a target="_blank" rel="noopener noreferrer" class="footer-content__link" href="/dummy.pdf">
            Our documents
          </a>

          <a target="_blank" rel="noopener noreferrer" class="footer-content__link" href="/dummy.pdf">
            Terms of use
          </a> -->

          <a target="_blank" rel="noopener noreferrer" class="footer-content__link"
            href="/RISKPOOLS PRIVACY POLICY.pdf">
            Privacy Policy
          </a>
          <a target="_blank" rel="noopener noreferrer" class="footer-content__link"
            href="/Business Proposal Doc.pdf">
            Business Proposal Doc
          </a>
        </div>

        <div class="footer-content__company-label col-lg-2 col-md-2 col-xs-12 text-right">
          (с) Riskpools 2024
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
  @import "./styles.module";
</style>
