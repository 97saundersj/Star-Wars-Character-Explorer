export interface Participant {
  id: number;
  name: string;
  teamId: number;
}

export interface Team {
  id: number;
  name: string;
  participants?: Participant[];
} 