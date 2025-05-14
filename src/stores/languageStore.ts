import { defineStore } from 'pinia'

export type Language = 'en' | 'au'

interface LanguageState {
  currentLanguage: Language
}

const translations = {
  en: {
    appTitle: 'Star Wars Characters',
    birthYear: 'Birth Year',
    gender: 'Gender',
    height: 'Height',
    mass: 'Mass',
    hairColor: 'Hair Color',
    skinColor: 'Skin Color',
    eyeColor: 'Eye Color',
    viewDetails: 'View Details',
    writeReview: 'Write a Review',
    yourName: 'Your Name',
    dateWatched: 'Date Watched',
    reviewDetails: 'Review Details',
    rating: 'Rating',
    submitReview: 'Submit Review',
    reviews: 'Reviews',
    watchedOn: 'Watched on',
    reviewedBy: 'Reviewed by',
    backToCharacters: 'Back to Characters',
    loading: 'Loading...',
    error: 'Failed to fetch characters',
    on: 'on'
  },
  au: {
    appTitle: 'STAR WARS',
    birthYear: 'BIRTH YEAR',
    gender: 'GENDER',
    height: 'HEIGHT',
    mass: 'MASS',
    hairColor: 'HAIR COLOR',
    skinColor: 'SKIN COLOR',
    eyeColor: 'EYE COLOR',
    viewDetails: 'VIEW DETAILS',
    writeReview: 'WRITE REVIEW',
    yourName: 'YOUR NAME',
    dateWatched: 'DATE WATCHED',
    reviewDetails: 'REVIEW DETAILS',
    rating: 'RATING',
    submitReview: 'SUBMIT REVIEW',
    reviews: 'REVIEWS',
    watchedOn: 'WATCHED ON',
    reviewedBy: 'REVIEWED BY',
    backToCharacters: 'BACK TO CHARACTERS',
    loading: 'LOADING...',
    error: 'FAILED TO FETCH CHARACTERS',
    on: 'ON'
  }
}

export const useLanguageStore = defineStore('language', {
  state: (): LanguageState => ({
    currentLanguage: 'en'
  }),

  actions: {
    setLanguage(language: Language) {
      this.currentLanguage = language
      document.documentElement.setAttribute('data-language', language)
    }
  },

  getters: {
    t: (state) => (key: keyof typeof translations.en) => {
      return translations[state.currentLanguage][key]
    }
  }
})
