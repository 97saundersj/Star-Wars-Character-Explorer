<template>
  <v-row class="align-center justify-center my-4">
    <v-col cols="12" sm="auto" class="mb-2 mb-sm-0 d-flex align-center">
      <div class="d-flex align-center justify-center">
        <span class="me-2 font-weight-medium">{{ languageStore.t('displayLimit') }}:</span>
        <v-select
          v-model="store.pageSize"
          :items="[
            { title: '12', value: '12' },
            { title: '24', value: '24' },
            { title: '48', value: '48' },
            { title: '96', value: '96' },
            { title: '1000', value: '1000' },
          ]"
          item-title="title"
          item-value="value"
          density="compact"
          variant="outlined"
          hide-details
          class="w-auto"
          @update:model-value="handlePageSizeChange"
        ></v-select>
      </div>
    </v-col>
    <v-col cols="12" sm="auto" class="d-flex align-center">
      <v-pagination
        v-if="store.pageSize !== 1000"
        v-model="store.currentPage"
        :length="store.totalPages"
        :total-visible="5"
        @update:model-value="(page) => store.fetchCharacters(Number(page))"
        density="compact"
        class="d-flex flex-wrap justify-center gap-1 mb-0"
      ></v-pagination>
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'

const store = useCharacterStore()
const languageStore = useLanguageStore()

const handlePageSizeChange = async () => {
  await store.fetchCharacters(1)
}
</script>

<style scoped>
:deep(.page-link) {
  background-color: var(--bg-dark) !important;
  border-color: #6c757d !important;
}

:deep(.page-item.active .page-link) {
  background-color: #0d6efd !important;
  border-color: #0d6efd !important;
}

:deep(.page-item.disabled .page-link) {
  background-color: var(--bg-dark) !important;
  border-color: #6c757d !important;
  opacity: 0.5;
}
</style>
