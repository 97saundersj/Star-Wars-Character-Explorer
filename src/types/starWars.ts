export interface StarWarsCharacter {
  _id: string;
  name: string;
  description: string;
  image: string;
  __v: number;
}

export interface Character extends StarWarsCharacter {
  isLiked: boolean;
}

export interface CharacterReview {
  id: string;
  characterName: string;
  reviewerName: string;
  watchDate: string;
  reviewDetails: string;
  rating: number;
  createdAt: string;
}

export interface CharacterState {
  characters: Character[];
  loading: boolean;
  error: string | null;
  reviews: CharacterReview[];
  currentPage: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  searchQuery: string;
  pageSize: number;
  info: {
    total: number;
    page: number;
    limit: number;
    next: string | null;
    prev: string | null;
  };
}
