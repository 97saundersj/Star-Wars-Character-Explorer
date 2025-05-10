import type { Team } from '../types/Types';

class Api {
  async getTeamById(teamId: number): Promise<Team> {
    // TODO: Implement actual API call
    return {
      id: teamId,
      name: 'Sample Team',
      participants: []
    };
  }
}

export const api = new Api(); 