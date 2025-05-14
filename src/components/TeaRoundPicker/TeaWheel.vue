<template>
  <div class="card m-2" :class="{ 'bg-dark text-light': isDarkMode }">
    <div class="card-body">
      <h5 class="card-title">Pick Tea Maker</h5>
      <button
        class="btn btn-success btn-lg w-100 mb-3"
        @click="pickTeaMaker"
        :disabled="isSpinning || !participants || participants.length === 0 || !teamId"
      >
        Pick Tea Maker
      </button>
    </div>

    <!-- Winner Modal -->
    <div 
      class="modal fade" 
      :class="{ show: modalVisible }" 
      tabindex="-1" 
      :style="{ display: modalVisible ? 'block' : 'none' }"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content" :class="{ 'bg-dark text-light': isDarkMode }">
          <div class="modal-header" :class="{ 'border-secondary': isDarkMode }">
            <h5 class="modal-title">Tea Maker Selected</h5>
            <button type="button" class="btn-close" :class="{ 'btn-close-white': isDarkMode }" @click="handleClose"></button>
          </div>
          <div class="modal-body text-center">
            <div v-if="selectedParticipant" class="alert text-center mt-3" :class="isDarkMode ? 'alert-dark' : 'alert-success'">
              🎉 {{ selectedParticipant.name }} will make the tea! 🎉
            </div>
          </div>
          <div class="modal-footer" :class="{ 'border-secondary': isDarkMode }">
            <button 
              v-if="selectedParticipant" 
              class="btn btn-primary" 
              @click="pickTeaMaker"
            >
              Pick Again?
            </button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="modalVisible" class="modal-backdrop fade show"></div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import { api } from '@/services/api'
import type { Participant } from '../../types/Types'

const router = useRouter()
const toast = useToast()

const props = defineProps<{
  participants: Participant[]
  teamId: number | null
  isDarkMode?: boolean
}>()

const emit = defineEmits<{
  (e: 'tea-maker-picked'): void
}>()

const isSpinning = ref(false)
const modalVisible = ref(false)
const selectedParticipant = ref<Participant | null>(null)

const pickTeaMaker = async () => {
  if (!props.teamId || props.participants.length === 0) {
    toast.error('Please add participants first')
    return
  }

  try {
    isSpinning.value = true
    console.log('Adding tea round for team:', props.teamId)
    const participantId = await api.addTeaRound(props.teamId)
    console.log('Tea round added, selected participant:', participantId)
    
    const selected = props.participants.find(p => p.id === participantId)
    if (!selected) throw new Error('Selected participant not found')

    selectedParticipant.value = selected
    console.log('Emitting tea-maker-picked event')
    emit('tea-maker-picked')
    
    // Add a slight delay to ensure the event has time to propagate
    setTimeout(() => {
      modalVisible.value = true
      isSpinning.value = false
    }, 100)
  } catch (error) {
    console.error("Error picking tea maker:", error)
    toast.error("Error picking tea maker. Please try again.")
    isSpinning.value = false
  }
}

const handleClose = () => {
  isSpinning.value = false
  modalVisible.value = false
}
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 1040;
  transition: opacity 0.3s ease;
}

.modal {
  z-index: 1050;
  transition: opacity 0.3s ease;
}

.modal.show {
  display: block;
}

.modal-content {
  background: white;
  border-radius: 15px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  transition: all 0.3s ease;
}

.modal-content.bg-dark {
  background: #212529;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.4);
}

.btn-success {
  background-color: #28a745;
  border-color: #28a745;
  transition: all 0.3s ease;
}

.btn-success:hover:not(:disabled) {
  background-color: #218838;
  border-color: #1e7e34;
}

.btn-success:disabled {
  background-color: #6c757d;
  border-color: #6c757d;
}

.alert-success {
  background-color: #d4edda;
  border-color: #c3e6cb;
  color: #155724;
  transition: all 0.3s ease;
}

.alert-dark {
  background-color: #343a40;
  border-color: #454d55;
  color: #e9ecef;
}

.card.bg-dark {
  background-color: #212529;
  border-color: #454d55;
}

.border-secondary {
  border-color: #454d55 !important;
}
</style> 