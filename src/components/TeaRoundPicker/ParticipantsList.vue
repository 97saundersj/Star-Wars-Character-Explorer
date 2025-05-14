<template>
  <div data-testid="participants-list" class="card bg-dark text-light m-2" v-if="teamId">
    <div class="card-body">
      <h5 class="card-title">Participants</h5>
      <form @submit.prevent="handleAddParticipant" class="mb-3">
        <div class="input-group">
          <input
            type="text"
            v-model="newParticipant"
            placeholder="Enter participant name"
            class="form-control bg-dark text-light border-secondary"
          />
          <button type="submit" class="btn btn-primary">Add</button>
        </div>
      </form>

      <h6 class="card-subtitle mb-2 text-secondary">Current Participants:</h6>
      <ul class="list-group list-group-flush">
        <ParticipantItem
          v-for="(participant, index) in participants"
          :key="participant.id ?? index"
          :participant="participant"
          :index="index"
          :handle-preferred-tea-change="handlePreferredTeaChange"
          :handle-remove-participant="handleRemoveParticipant"
        />
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { Team, Participant } from '../../types/Types';
import { api } from '../../services/api';
import { useToast } from 'vue-toastification';
import ParticipantItem from './ParticipantItem.vue';

const props = defineProps<{
  teamId: number | null;
  participants: Participant[];
  setParticipants: (participants: Participant[]) => void;
  onParticipantAdded: () => void;
}>();

const newParticipant = ref<string>('');
const toast = useToast();

const addParticipantToAPI = async (participantName: string): Promise<void> => {
  if (!props.teamId) return;

  const participant: Participant = {
    id: 0,
    name: participantName,
    teamId: props.teamId,
    preferredTea: "",
  };

  try {
    const createdParticipant = await api.createParticipant(participant);
    if (!createdParticipant || !createdParticipant.id) throw new Error("Participant invalid");
    await api.addParticipantToTeamById(props.teamId, createdParticipant.id);
  } catch (error) {
    console.error("Error adding participant:", error);
    toast.error('Error adding participant. Please try again.');
  }
};

const handleAddParticipant = async (): Promise<void> => {
  const trimmedName = newParticipant.value.trim();

  if (trimmedName && !props.participants.some(p => p.name === trimmedName)) {
    await addParticipantToAPI(trimmedName);

    const newParticipantData: Participant = {
      teamId: props.teamId!,
      name: trimmedName,
      preferredTea: null,
    };

    props.setParticipants([...props.participants, newParticipantData]);
    newParticipant.value = '';
    props.onParticipantAdded();
  }
};

const handleRemoveParticipant = async (index: number): Promise<void> => {
  if (!props.teamId) return;

  const participantToRemove = props.participants[index];
  if (!participantToRemove || !participantToRemove.id) return;

  try {
    await api.removeParticipant(props.teamId, participantToRemove.id);
    props.setParticipants(props.participants.filter((_, i) => i !== index));
  } catch (error) {
    console.error("Error removing participant:", error);
    toast.error('Error removing participant. Please try again.');
  }
};

const handlePreferredTeaChange = async (id: number | null | undefined, newTea: string): Promise<void> => {
  try {
    if (!id) throw new Error("Participant invalid");

    const participantToUpdate = props.participants.find(participant => participant.id === id);
    if (!participantToUpdate) throw new Error("Participant not found");

    const updatedParticipant = { ...participantToUpdate, preferredTea: newTea };
    await api.updateParticipant(updatedParticipant);

    props.setParticipants(props.participants.map((participant) =>
      participant.id === id ? updatedParticipant : participant
    ));

  } catch (error) {
    console.error("Error updating preferred tea:", error);
    toast.error('Error updating preferred tea. Please try again.');
  }
};
</script>

<style scoped>
</style> 