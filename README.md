# Star Wars Character Web App

A Vue 3 application that allows users to browse Star Wars characters, view their details, and submit reviews.

## Features

- Browse Star Wars characters from the SWAPI
- View detailed information about each character
- Like/unlike characters
- Submit reviews for characters including:
  - Reviewer name
  - Watch date
  - Review details
  - Rating (1-10)
- View existing reviews for characters
- Local storage for reviews (since the API doesn't support review submission)

## Tech Stack

- Vue 3
- TypeScript
- Vuetify 3
- Pinia for state management
- Vue Router
- Axios for API calls

## Project Setup

```bash
# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build

# Run unit tests
npm run test:unit

# Run end-to-end tests
npm run test:e2e
```

## API Integration

The application uses the Star Wars API (SWAPI) for character data:
- Base URL: https://swapi.dev/api/
- Endpoints used:
  - /people/ - List of characters
  - /people/{id}/ - Character details


## Project Structure

```
src/
├── assets/         # Static assets
├── components/     # Reusable Vue components
├── router/         # Vue Router configuration
├── stores/         # Pinia stores
├── types/          # TypeScript type definitions
└── views/          # Vue page components
```

## Development Notes

- The application uses Vuetify for UI components and styling
- State management is handled by Pinia
- TypeScript is used for type safety
- The review submission is designed to fail as per requirements, with local storage as a fallback