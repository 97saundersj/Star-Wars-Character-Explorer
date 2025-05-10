<template>
  <div class="tea-wheel">
    <h2>Tea Wheel</h2>
    <div class="wheel-container">
      <div
        v-if="participants.length"
        class="wheel"
        :class="{ spinning: isSpinning }"
        @click="spinWheel"
      >
        <div
          v-for="(participant, index) in participants"
          :key="participant.id"
          class="wheel-segment"
          :style="getSegmentStyle(index)"
        >
          {{ participant.name }}
        </div>
      </div>
      <div v-else class="no-participants">
        Add participants to spin the wheel
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import type { Participant } from '../types/Types'

const props = defineProps<{
  teamId: number | null
  participants: Participant[]
}>()

const emit = defineEmits<{
  (e: 'tea-maker-picked'): void
}>()

const isSpinning = ref(false)

const getSegmentStyle = (index: number) => {
  const totalParticipants = props.participants.length
  const rotation = (360 / totalParticipants) * index
  return {
    transform: `rotate(${rotation}deg) translate(50%)`
  }
}

const spinWheel = () => {
  if (isSpinning.value || !props.participants.length) return

  isSpinning.value = true
  setTimeout(() => {
    isSpinning.value = false
    emit('tea-maker-picked')
  }, 3000)
}
</script>

<style scoped>
.tea-wheel {
  margin-bottom: 1rem;
}

.wheel-container {
  position: relative;
  width: 300px;
  height: 300px;
  margin: 0 auto;
}

.wheel {
  position: relative;
  width: 100%;
  height: 100%;
  border-radius: 50%;
  border: 2px solid #333;
  cursor: pointer;
  transition: transform 3s cubic-bezier(0.17, 0.67, 0.83, 0.67);
}

.wheel.spinning {
  transform: rotate(1800deg);
}

.wheel-segment {
  position: absolute;
  width: 50%;
  height: 50%;
  transform-origin: 100% 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 0.8rem;
  text-align: center;
  padding: 0.5rem;
}

.no-participants {
  text-align: center;
  color: #666;
  font-style: italic;
  padding: 1rem;
}
</style> 