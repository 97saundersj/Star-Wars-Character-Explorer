<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { BNavbar, BNavbarBrand, BNavbarNav, BNavItem, BContainer } from 'bootstrap-vue-next'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css'
</script>

<template>
  <div class="min-vh-100 d-flex flex-column bg-dark text-light">
    <BNavbar toggleable="lg" type="dark" variant="dark" fixed="top" class="shadow-sm">
      <BContainer class="d-flex align-items-center">
        <BNavbarBrand to="/" class="d-flex align-items-center">
          <img alt="Vue logo" src="@/assets/logo.svg" width="30" height="30" class="me-2" />
          Vue App
        </BNavbarBrand>

        <BNavbarNav class="ms-4">
          <BNavItem to="/" exact>Home</BNavItem>
          <BNavItem to="/about">About</BNavItem>
          <BNavItem to="/tea-rounds">Tea</BNavItem>
        </BNavbarNav>
      </BContainer>
    </BNavbar>

    <div class="mt-5 pt-4 flex-grow-1 bg-dark">
      <BContainer class="bg-dark">
        <RouterView />
      </BContainer>
    </div>
  </div>
</template>

<style scoped>
:deep(.router-link-active) {
  color: #42b983 !important;
}
</style>
