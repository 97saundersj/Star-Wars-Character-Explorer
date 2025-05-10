<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import 'bootstrap/dist/css/bootstrap.min.css'
</script>

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
          <ul class="navbar-nav ms-auto">
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
              <RouterLink class="nav-link px-3" to="/about">
                <i class="bi bi-info-circle me-1"></i>About
              </RouterLink>
            </li>
          </ul>
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
