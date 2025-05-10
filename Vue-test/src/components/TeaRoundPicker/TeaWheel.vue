<template>
  <div class="card m-2">
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
    <div class="modal fade" :class="{ show: modalVisible }" tabindex="-1" :style="{ display: modalVisible ? 'block' : 'none' }">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Tea Maker Selected</h5>
            <button type="button" class="btn-close" @click="handleClose"></button>
          </div>
          <div class="modal-body text-center">
            <div v-if="selectedParticipant" class="alert alert-success text-center mt-3">
              🎉 {{ selectedParticipant.name }} will make the tea! 🎉
            </div>
          </div>
          <div class="modal-footer">
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
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import { api } from '@/services/api'

interface Participant {
  id: number
  name: string
}

const router = useRouter()
const toast = useToast()

const props = defineProps<{
  participants: Participant[]
  teamId: number | null
}>()

const emit = defineEmits<{
  (e: 'pickedTeaMaker'): void
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
    console.log('Emitting pickedTeaMaker event')
    emit('pickedTeaMaker')
    modalVisible.value = true
    isSpinning.value = false
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
}

.modal {
  z-index: 1050;
}

.modal.show {
  display: block;
}

.modal-content {
  background: white;
  border-radius: 15px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
}

.btn-success {
  background-color: #28a745;
  border-color: #28a745;
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
}
</style> 