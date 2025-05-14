<template>
  <BContainer class="py-4">
    <div class="mb-5">
      <h1 class="text-light display-4 fw-bold mb-3">About</h1>
    </div>

    <div class="text-center">
      <BButton
        href="https://github.com/97saundersj/Vue-Test"
        target="_blank"
        rel="noopener noreferrer"
        variant="outline-secondary"
        class="text-light border-secondary"
        size="lg"
      >
        <i class="bi bi-github me-2"></i>Check out the project on GitHub
      </BButton>
    </div>
  </BContainer>
</template>

<script setup lang="ts">
import {
  BContainer,
  BButton
} from 'bootstrap-vue-next'
</script>
