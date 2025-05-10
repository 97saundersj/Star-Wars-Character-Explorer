<template>
  <li :data-testid="`participant-item-${participant.id}`" class="list-group-item bg-dark text-light border-secondary">
    <div class="row align-items-center">
      <div class="col-3">
        <span>{{ participant.name }}</span>
      </div>
      <div class="col">
        <input
          type="text"
          name="preferredTea"
          class="form-control bg-dark text-light border-secondary"
          v-model="inputValue"
          placeholder="Preferred Tea"
          @blur="handleBlur"
        />
      </div>
      <div class="col-auto">
        <button
          class="btn btn-danger btn-sm"
          @click="handleRemoveParticipant(index)"
        >
          Remove
        </button>
      </div>
    </div>
  </li>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue';
import type { Participant } from '../../types/Types';

const props = defineProps<{
  index: number;
  participant: Participant;
  handlePreferredTeaChange: (id: number | null | undefined, newTea: string) => Promise<void>;
  handleRemoveParticipant: (index: number) => Promise<void>;
}>();

const inputValue = ref<string>(props.participant.preferredTea || '');

// Update inputValue when participant.preferredTea changes
watch(() => props.participant.preferredTea, (newValue) => {
  inputValue.value = newValue || '';
});

const handleBlur = (): void => {
  props.handlePreferredTeaChange(props.participant.id, inputValue.value);
};
</script>