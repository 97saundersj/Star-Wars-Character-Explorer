export interface Character {
  name: string
  height: string
  mass: string
  hair_color: string
  skin_color: string
  eye_color: string
  birth_year: string
  gender: string
  homeworld: string
  films: string[]
  species: string[]
  vehicles: string[]
  starships: string[]
  created: string
  edited: string
  url: string
  isLiked?: boolean
}

export interface CharacterReview {
  id: string
  characterName: string
  reviewerName: string
  watchDate: string
  reviewDetail: string
  rating: number
  createdAt: string
}

export interface CharacterState {
  characters: Character[]
  loading: boolean
  error: string | null
  reviews: CharacterReview[]
}
