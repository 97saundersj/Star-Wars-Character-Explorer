<template>
  <BRow class="align-items-center justify-content-center" :class="{ 'mt-4': isBottom, 'mb-3': true }">
    <BCol cols="12" sm="auto" class="mb-2 mb-sm-0">
      <BFormGroup class="d-flex align-items-center justify-content-center mb-0">
        <label :for="pageSizeId" class="text-light me-2 fw-medium">{{ languageStore.t('displayLimit') }}:</label>
        <BFormSelect
          :id="pageSizeId"
          class="bg-dark text-light border-secondary"
          style="width: auto; min-width: 80px;"
          v-model="store.pageSize"
          @change="handlePageSizeChange"
          :options="[
            { value: '12', text: '12' },
            { value: '24', text: '24' },
            { value: '48', text: '48' },
            { value: '96', text: '96' },
            { value: '1000', text: languageStore.t('showAll') }
          ]"
        />
      </BFormGroup>
    </BCol>
    <BCol cols="12" sm="auto">
      <BPagination
        v-if="store.pageSize !== 1000"
        v-model="store.currentPage"
        :total-rows="store.totalPages"
        :per-page="1"
        align="center"
        @update:model-value="(page) => store.fetchCharacters(Number(page))"
        size="lg"
        class="pagination-mobile"
      >
        <template #first-text>
          <i class="bi bi-chevron-double-left"></i>
        </template>
        <template #prev-text>
          <i class="bi bi-chevron-left"></i>
        </template>
        <template #next-text>
          <i class="bi bi-chevron-right"></i>
        </template>
        <template #last-text>
          <i class="bi bi-chevron-double-right"></i>
        </template>
      </BPagination>
    </BCol>
  </BRow>
</template>

<script setup lang="ts">
import { useCharacterStore } from '@/stores/characterStore'
import { useLanguageStore } from '@/stores/languageStore'
import {
  BRow,
  BCol,
  BFormGroup,
  BFormSelect,
  BPagination
} from 'bootstrap-vue-next'

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
.pagination-mobile {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 0.25rem;
}

:deep(.page-link) {
  padding: 0.5rem 0.75rem;
}

@media (max-width: 576px) {
  :deep(.page-link) {
    padding: 0.25rem 0.375rem;
    font-size: 0.875rem;
  }

  :deep(.pagination) {
    margin: 0;
    flex-wrap: wrap;
  }

  :deep(.page-item) {
    margin: 0;
  }
}
</style>
