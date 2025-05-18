# Star Wars Character Web App

This repository hosts the Front End and Back End code for a web application that allows users to browse Star Wars characters, view their details, and submit reviews.

## Features

- Browse Star Wars characters from the [Star Wars Databank API](https://starwars-databank.vercel.app/)
- View detailed information about each character
- Like/unlike characters
- (Attempt to) Submit reviews for characters
- Support for both English and Aurebesh language options

## Frontend [![Build & deploy Frontend](https://github.com/97saundersj/Star-Wars-Character-Explorer/actions/workflows/buildTestDeploy-frontend.yml/badge.svg)](https://github.com/97saundersj/Star-Wars-Character-Explorer/actions/workflows/buildTestDeploy-frontend.yml)

[Vue Web App](https://salmon-glacier-0858cd303.6.azurestaticapps.net/)

### Running the Frontend Project

**Open the Project in VS Code:**
1. Launch VS Code.
2. Open the `Frontend` folder.

**Install Dependencies:**
   Make sure you have [Node.js](https://nodejs.org/) installed. Then run:
   ```
   npm install
   ```

**Start the Development Server:**
   ```
   npm run dev
   ```

### Running Tests

**Run Tests:**
   To execute tests, use:
   ```
   npm run test:unit
   ```

## Backend [![Build & deploy Backend](https://github.com/97saundersj/Star-Wars-Character-Explorer/actions/workflows/buildTestDeploy-backend.yml/badge.svg)](https://github.com/97saundersj/Star-Wars-Character-Explorer/actions/workflows/buildTestDeploy-backend.yml)

[Web API Swagger Page](https://starwarsapi-hxcqbxddd4hhacaq.ukwest-01.azurewebsites.net/swagger/index.html)

### Running the Backend Project

**Open the Solution in Visual Studio:**
1. Launch Visual Studio.
2. Open the solution file located in the `Backend` directory.

**Set the Startup Project:**
- Right-click on the API project in the Solution Explorer and select "Set as StartUp Project".

**Run the Project:**
- Press `F5` or click on the "Start" button to run the project. This will start the web API.

### Running Tests
To execute tests for the Back End:
1. Open the test project in Solution Explorer.
2. Right-click on the project and select "Run Tests" or use the Test Explorer to run all tests.
