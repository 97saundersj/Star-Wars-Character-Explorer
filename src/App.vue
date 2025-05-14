<template>
  <div class="min-vh-100 d-flex flex-column bg-dark text-light">
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark border-bottom border-secondary">
      <div class="container">
        <RouterLink class="navbar-brand fw-bold" to="/">
          <i class="bi bi-cup-hot me-2"></i>Tea Round Picker
        </RouterLink>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav me-auto">
            <li class="nav-item">
              <RouterLink class="nav-link px-3" to="/">
                <i class="bi bi-house-door me-1"></i>Home
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link px-3" to="/tea-rounds">
                <i class="bi bi-cup-hot me-1"></i>Tea Round
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link px-3" to="/star-wars">
                <i class="bi bi-stars me-1"></i>Star Wars
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link px-3" to="/about">
                <i class="bi bi-info-circle me-1"></i>About
              </RouterLink>
            </li>
          </ul>
          <div class="d-flex align-items-center">
            <div class="btn-group">
              <button
                class="btn btn-outline-light"
                :class="{ active: languageStore.currentLanguage === 'en' }"
                @click="languageStore.setLanguage('en')"
              >
                EN
              </button>
              <button
                class="btn btn-outline-light"
                :class="{ active: languageStore.currentLanguage === 'au' }"
                @click="languageStore.setLanguage('au')"
              >
                <span class="aurebesh">AU</span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </nav>

    <main class="flex-grow-1 py-4">
      <div class="container">
        <RouterView />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { useLanguageStore } from '@/stores/languageStore'
import 'bootstrap/dist/css/bootstrap.min.css'
import '@/assets/fonts.css'

const languageStore = useLanguageStore()
</script>

<style>
.v-application {
  background: #f5f5f5;
}

.btn-group .btn.active {
  background-color: #0d6efd;
  border-color: #0d6efd;
}

/* Apply Aurebesh font when language is set to Aurebesh */
html[data-language="au"] {
  font-family: 'Aurebesh', sans-serif !important;
}

html[data-language="au"] * {
  font-family: inherit !important;
}

/* Prevent EN button from changing font */
html[data-language="au"] .btn-group .btn:first-child {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif !important;
}
</style>
