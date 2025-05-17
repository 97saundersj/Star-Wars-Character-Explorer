import type { StarWarsCharacter, CharacterState } from '@/types/starWars';

const BASE_URL = import.meta.env.VITE_API_URL;

export const starwarsApi = {
  async getAllCharacters(page: number = 1, limit?: number, search?: string): Promise<{ info: CharacterState['info']; data: StarWarsCharacter[] }> {
    const searchParam = search ? `&search=${encodeURIComponent(search)}` : '';
    const limitParam = limit && limit > 0 ? `&limit=${limit}` : '';
    const url = `${BASE_URL}/characters?page=${page}${limitParam}${searchParam}`;
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error('Failed to fetch characters');
    }
    return response.json();
  },

  async getCharacterById(id: string): Promise<StarWarsCharacter> {
    const response = await fetch(`${BASE_URL}/characters/${id}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch character with id ${id}`);
    }
    return response.json();
  }
};
