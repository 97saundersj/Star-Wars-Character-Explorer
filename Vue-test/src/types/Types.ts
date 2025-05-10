export interface Participant {
  id: number;
  name: string;
  teamId: number;
}

export interface Team {
  id?: number;
  label: string;
  participants?: Participant[];
}

export interface TeaRound {
  id: number;
  teamId: number;
  date: string;
  participants: Participant[];
} 