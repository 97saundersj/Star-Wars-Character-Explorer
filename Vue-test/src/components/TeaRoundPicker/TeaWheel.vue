<template>
  <div class="tea-wheel-container">
    <button 
      @click="startSpinning" 
      :disabled="mustStartSpinning || participants.length === 0"
      class="spin-button"
    >
      {{ isSpinning ? 'Spinning...' : 'Spin the Wheel' }}
    </button>

    <!-- Wheel Modal -->
    <div v-if="isSpinning" class="modal-overlay">
      <div class="wheel-modal">
        <Roulette
          v-if="participants.length > 0"
          :items="wheelData"
          :wheel-result-index="prizeNumber"
          :first-item-index="0"
          :size="400"
          :centered-indicator="true"
          :indicator-position="'top'"
          :display-shadow="true"
          :duration="4"
          :easing="'ease'"
          :display-border="true"
          :display-indicator="true"
          @wheel-end="onStopSpinning"
        />
      </div>
    </div>

    <!-- Winner Modal -->
    <div v-if="showWinnerModal" class="modal-overlay">
      <div class="modal-content">
        <h2>Tea Round Winner!</h2>
        <p>{{ selectedParticipant }} has been selected to make the tea!</p>
        <button @click="closeModal" class="modal-button">Spin Again</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import { Roulette } from 'vue3-roulette'

const router = useRouter()
const toast = useToast()

const props = defineProps<{
  participants: string[]
}>()

const mustStartSpinning = ref(false)
const prizeNumber = ref(0)
const showWinnerModal = ref(false)
const selectedParticipant = ref('')
const isSpinning = ref(false)

const wheelData = computed(() => {
  return props.participants
})

const startSpinning = () => {
  if (props.participants.length === 0) {
    toast.error('Please add participants first')
    return
  }
  isSpinning.value = true
  mustStartSpinning.value = true
  prizeNumber.value = Math.floor(Math.random() * props.participants.length)
}

const onStopSpinning = () => {
  mustStartSpinning.value = false
  isSpinning.value = false
  selectedParticipant.value = props.participants[prizeNumber.value]
  showWinnerModal.value = true
  toast.success(`${selectedParticipant.value} has been selected!`)
}

const closeModal = () => {
  showWinnerModal.value = false
}

const goToParticipants = () => {
  router.push('/participants')
}
</script>

<style scoped>
.tea-wheel-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
  min-height: 100vh;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
}

.spin-button {
  background: #FF8B26;
  color: white;
  border: none;
  padding: 1rem 2rem;
  border-radius: 25px;
  font-size: 1.2rem;
  cursor: pointer;
  transition: all 0.3s ease;
  box-shadow: 0 4px 15px rgba(255, 139, 38, 0.3);
}

.spin-button:hover:not(:disabled) {
  background: #FF7B16;
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(255, 139, 38, 0.4);
}

.spin-button:disabled {
  background: #ccc;
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  backdrop-filter: blur(5px);
}

.wheel-modal {
  background: white;
  padding: 2rem;
  border-radius: 20px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  animation: modalFadeIn 0.3s ease-out;
}

.modal-content {
  background: white;
  padding: 2rem;
  border-radius: 15px;
  text-align: center;
  max-width: 90%;
  width: 400px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
  animation: modalFadeIn 0.3s ease-out;
}

.modal-content h2 {
  color: #FF8B26;
  margin-bottom: 1rem;
}

.modal-button {
  background: #FF8B26;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  border-radius: 25px;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
  margin-top: 1rem;
}

.modal-button:hover {
  background: #FF7B16;
  transform: translateY(-2px);
}

@keyframes modalFadeIn {
  from {
    opacity: 0;
    transform: scale(0.95);
  }
  to {
    opacity: 1;
    transform: scale(1);
  }
}
</style> 