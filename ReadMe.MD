# Task Management System (Blazor Server + ASP.NET Core Web API)

A clean-architecture demo with **Blazor Server** (Bootstrap 5 UI) and an **ASP.NET Core Web API (.NET 8)** using an **in-memory database**. Dockerfiles and a `docker-compose.yml` are included to run both services together.

---

## Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [1) Clone](#1-clone)
  - [2) Restore Packages](#2-restore-packages)
  - [3) Run with Docker](#4-run-with-docker)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

## Features

- Task CRUD (create, read, update, delete)
- Sorting/filtering list, confirmation dialog, Bootstrap 5 UI
- In-memory DB (no external setup)
- Error handling middleware & BE validation
- Pagination-ready API
- Clean Architecture: Domain / Application / Infrastructure / API
- Dockerized: API + Blazor Server

---

## Tech Stack

- **Frontend:** Blazor Server (.NET 8), Bootstrap 5  
- **Backend:** ASP.NET Core Web API (.NET 8)  
- **Data:** EF Core InMemory  
- **Containers:** Docker & Docker Compose

---

## Project Structure

```
/ (solution root)
  docker-compose.yml
  TaskManagementSystem.Domain/
  TaskManagementSystem.Application/
  TaskManagementSystem.Infrastructure/
  TaskManagementSystem.API/
    Dockerfile
  Pascual.TaskManagementSystem.Blazor/
    Dockerfile
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- Git

---

## Getting Started

### 1) Clone

```bash
git clone <your-repo-url> TaskManagementSystem
cd TaskManagementSystem
```

### 2) Restore Packages

```bash
dotnet restore
```

> Or open the solution in Visual Studio / Rider to restore automatically.

---

### 3) Run with Docker

Everything is preconfigured via **Dockerfiles** and **docker-compose**.

#### 3.1 Build & Run (first time)

```bash
# from solution root
docker compose build
docker compose up 
```

- UI: **http://localhost:8081**  
- API Swagger: **http://localhost:8080/swagger**

> Inside Docker, the UI calls the API via `http://api:8080/` (Compose service name).

#### 3.2 Subsequent Runs (no rebuild)

```bash
docker compose up -d --no-build
```

#### 3.3 Stop / Remove

```bash
docker compose down
```

#### 3.4 View Logs

```bash
docker compose logs -f api
docker compose logs -f blazor
```

---

## API Endpoints

| Method | Route             | Description          |
|-------:|-------------------|----------------------|
| GET    | `/api/tasks`      | Get all (supports pagination) |
| GET    | `/api/tasks/{id}` | Get by id            |
| POST   | `/api/tasks`      | Create               |
| PUT    | `/api/tasks/{id}` | Update               |
| DELETE | `/api/tasks/{id}` | Delete               |

Swagger: `http://localhost:8080/swagger` (Docker) or your local API port.

---

## Configuration

- **Blazor → API base URL:** `ApiBaseUrl` (env var or `appsettings.json`).
- **CORS:** The API enables a permissive policy for development when UI/API are on different ports.
- **Database:** EF Core **InMemory** provider.
- **Docker Ports (default):**
  - API → `8080:8080`
  - Blazor → `8081:8081`  
  Change in `docker-compose.yml` as needed.

---

## Troubleshooting

- **`pull access denied for tms-api` in compose**  
  You referenced an image tag that doesn’t exist locally. Use:
  ```bash
  docker compose up -d --build
  ```
  or build once and reference the exact tag in `docker-compose.yml`.

- **UI can’t reach API (CORS/404)**  
  - Confirm API is running (Swagger reachable).
  - Ensure `ApiBaseUrl` in Blazor matches the API’s URL/port (or `http://api:8080/` in Docker).

- **Blazor events not firing (local .NET 8 Blazor Server)**  
  Ensure `App.razor` includes:
  ```razor
  <Routes @rendermode="Microsoft.AspNetCore.Components.Web.RenderMode.InteractiveServer" />
  <script src="_framework/blazor.web.js"></script>
  ```

- **Mobile menu not opening (offcanvas)**  
  Use Bootstrap bundle JS or the provided pure-Blazor drawer. For bundle:
  ```html
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
          integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
          crossorigin="anonymous"></script>
  ```

- **Ports already in use**  
  Change the **left** side of the port mappings in `docker-compose.yml` (e.g., `8082:8081`).

---

## License

MIT (or your preferred license).
