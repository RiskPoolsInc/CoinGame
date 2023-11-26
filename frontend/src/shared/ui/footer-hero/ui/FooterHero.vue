<script setup lang="ts"></script>

<template>
  <div class="footer-hero">
    <div class="row container justify-between">
      <div class="col-lg-5 col-xs-12">
        <div class="row footer-hero__list">
          <div class="col-lg-1 col-xs-2 my-auto">
            <q-icon name="telegram" size="sm" />
          </div>

          <div class="col-lg-11 col-xs-10 footer-hero__item">Telegram chat</div>

          <div class="col-lg-1 col-xs-2 my-auto">
            <q-icon name="mail" size="sm" />
          </div>

          <div class="col-lg-11 col-xs-10 footer-hero__item">
            Tech support:
            <a
              class="footer-hero__link"
              href="mailto:support_coin@riskpools.io"
            >
              support_coin@riskpools.io
            </a>
          </div>
        </div>
      </div>
      <div class="col-lg-5 col-xs-12">
        <div class="row footer-hero__list">
          <div class="col-lg-1 col-xs-2 my-auto">
            <q-icon name="people" size="sm" />
          </div>
          <div class="col-lg-11 col-xs-10 footer-hero__item">
            Partnerships:
            <a class="footer-hero__link" href="mailto:offer@riskpools.io"
              >offer@riskpools.io</a
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
