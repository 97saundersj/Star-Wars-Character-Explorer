<template>
  <BRow
    class="align-items-center justify-content-center"
    :class="{ 'mt-4': isBottom, 'mb-3': true }"
  >
    <BCol cols="12" sm="auto" class="mb-2 mb-sm-0 d-flex align-items-center">
      <BFormGroup class="d-flex align-items-center justify-content-center mb-0">
        <label :for="pageSizeId" class="me-2 fw-medium mb-0"
          >{{ languageStore.t('displayLimit') }}:</label
        >
        <BFormSelect
          :id="pageSizeId"
          class="bg-dark border-secondary w-auto"
          v-model="store.pageSize"
          @change="handlePageSizeChange"
          :options="[
            { value: '12', text: '12' },
            { value: '24', text: '24' },
            { value: '48', text: '48' },
            { value: '96', text: '96' },
            { value: '1000', text: '1000' },
          ]"
        />
      </BFormGroup>
    </BCol>
    <BCol cols="12" sm="auto" class="d-flex align-items-center">
      <BPagination
        v-if="store.pageSize !== 1000"
        v-model="store.currentPage"
        :total-rows="store.totalPages"
        :per-page="1"
        align="center"
        @update:model-value="(page) => store.fetchCharacters(Number(page))"
        size="md"
        class="d-flex flex-wrap justify-content-center gap-1 mb-0"
        dark
      >
      </BPagination>
    </BCol>
  </BRow>
</template>

<script setup lang="ts">
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import { BRow, BCol, BFormGroup, BFormSelect, BPagination } from 'bootstrap-vue-next'

const props = defineProps<{
  isBottom?: boolean
}>()

const store = useCharacterStore()
const languageStore = useLanguageStore()

const pageSizeId = props.isBottom ? 'pageSizeBottom' : 'pageSize'

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
