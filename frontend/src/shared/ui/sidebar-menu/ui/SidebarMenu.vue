<script setup lang="ts"></script>

<template>
  <div class="sidebar-menu">
    <ul class="sidebar-menu__list">
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game">1x_How</router-link>
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >2x_Technology</router-link
        >
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >3x_Do_more</router-link
        >
      </li>
      <li class="sidebar-menu__item">
        <router-link class="sidebar-menu__link" to="/game"
          >4x_Get_in_touch</router-link
        >
      </li>
    </ul>
  </div>
</template>

<style scoped lang="scss">
@import "./styles.module";
</style>
