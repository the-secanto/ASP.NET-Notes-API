# ASP.NET Notes API

A small ASP.NET Core API for managing notes with PostgreSQL and Docker Compose support.

## Features

- CRUD endpoints for notes
- PostgreSQL persistence via EF Core
- Swagger UI for local development
- Docker Compose setup for the API and database
- One-off data seeding utility for sample records

## Prerequisites

- .NET SDK 10+
- Docker and Docker Compose
- PostgreSQL is provided by the Compose stack, so no local database install is required

## Local configuration

Create a local environment file from the sample:

```bash
cp .env.example .env
```

The default values are:

```env
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=notes_api
APP_PORT=8080
```

## Run with Docker Compose

Start the API and PostgreSQL:

```bash
docker compose up --build
```

The API will be available at:

- http://localhost:8080
- Swagger: http://localhost:8080/swagger

## Seed the database

Populate the database with sample notes:

```bash
docker compose --profile seed up --build seed
```

This runs the app in seed mode once and exits.

## Run locally without Docker

```bash
dotnet restore

dotnet run --project NotesApi/NotesApi.csproj
```

Set the connection string in a local development override file or environment variable before running:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5402;Database=notes_api;Username=postgres;Password=postgres"
```

## API endpoints

- GET /notes
- GET /notes/{id}
- POST /notes
- PUT /notes/{id}
- DELETE /notes/{id}
- GET /test

## Notes

- The Compose setup uses a Docker network so the API connects to PostgreSQL via the service name `postgres`.
- Local secrets and environment-specific values should remain in `.env` and not be committed to source control.
