import type { StarWarsCharacter, CharacterState } from '@/types/starWars';

const BASE_URL = 'https://starwars-databank-server.vercel.app/api/v1/characters';

export const starwarsApi = {
  async getAllCharacters(page: number = 1, limit?: number, search?: string): Promise<{ info: CharacterState['info']; data: StarWarsCharacter[] }> {
    const searchParam = search ? `&search=${encodeURIComponent(search)}` : '';
    const limitParam = limit && limit > 0 ? `&limit=${limit}` : '';
    const url = `${BASE_URL}?page=${page}${limitParam}${searchParam}`;
    console.log('API Request URL:', url);
    console.log('Parameters:', { page, limit, search });
    const response = await fetch(url);
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
