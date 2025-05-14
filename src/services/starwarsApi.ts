import type { StarWarsCharacter, CharacterState } from '@/types/starWars';

const BASE_URL = 'https://starwars-databank-server.vercel.app/api/v1/characters';

export const starwarsApi = {
  async getAllCharacters(page: number = 1, limit: number = 10, search?: string): Promise<{ info: CharacterState['info']; data: StarWarsCharacter[] }> {
    const searchParam = search ? `&search=${encodeURIComponent(search)}` : '';
    const response = await fetch(`${BASE_URL}?page=${page}&limit=${limit}${searchParam}`);
    if (!response.ok) {
      throw new Error('Failed to fetch characters');
    }
    return response.json();
  },

  async getCharacterById(id: string): Promise<StarWarsCharacter> {
    const response = await fetch(`${BASE_URL}/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch character with id ${id}`);
    }
    return response.json();
  }
};
