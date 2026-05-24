# TopicBoard API

A RESTful backend API for a topic-and-comments platform built with ASP.NET Core and C#.  
Users can create topics, leave comments, and manage their content. Admins have extended control over users and content moderation.

## 🚀 Live Demo

**Swagger UI:** [https://topicboard-api-railway-production.up.railway.app/swagger](https://topicboard-api-railway-production.up.railway.app/swagger)

### Test Credentials

| Role  | Email             | Password  |
|-------|-------------------|-----------|
| Admin | admin@gmail.com   | Admin123! |
| User  | nika@gmail.com    | Nika123!  |
| User  | gio@gmail.com     | Gio123!   |

> **How to test:** Open Swagger UI → click `/api/Auth/login` → enter credentials → copy the JWT token → click **Authorize** (🔒 button) → paste token as `Bearer <token>`

---

## Tech Stack

- **ASP.NET Core 8** — Web API framework
- **C# / Entity Framework Core** — Data access and ORM
- **PostgreSQL** — Relational database
- **ASP.NET Core Identity** — User management
- **JWT** — Authentication and authorization
- **Swagger** — API documentation
- **Railway** — Cloud deployment

---

## Architecture

The project follows a layered architecture split across multiple projects:

```
Topic.API            → Controllers, middleware, app entry point
Topic.Service        → Business logic, custom exceptions
Topic.Repositories   → Data access layer
Topic.Data           → DbContext, migrations, data seeding
Topic.Models         → DTOs (request/response models)
Topic.Contracts      → Interfaces
FinalProject         → Domain entities (User, TopicEntity, Comments)
```

---

## Features

- JWT-based authentication with role-based access control (Admin / Customer)
- Full CRUD for topics and comments
- Admin panel: user management, block/unblock users, register admins
- DTO-based request/response separation
- Custom exception handling via middleware
- Background service for topic state management
- Data seeding for testing
- Deployed on Railway with PostgreSQL

---

## API Endpoints

### Auth — `/api/Auth`

| Method | Endpoint          | Auth       | Description              |
|--------|-------------------|------------|--------------------------|
| POST   | `/login`          | Public     | Login, returns JWT token |
| POST   | `/register`       | Public     | Register new user        |
| POST   | `/registeradmin`  | Admin only | Register new admin       |
| POST   | `/block`          | Admin only | Block a user             |
| POST   | `/unBlock`        | Admin only | Unblock a user           |
| GET    | `/usersInfo`      | Admin only | Get all users            |
| GET    | `/GetUserByEmail` | Authorized | Get user by email        |
| PUT    | `/UserUpdate`     | Admin only | Update user info         |

### Topics & Comments — `/api/topics`

| Method | Endpoint                         | Auth       | Description                  |
|--------|----------------------------------|------------|------------------------------|
| GET    | `/AllTopics`                     | Public     | Get all topics               |
| GET    | `/AllTopicsWithComments`         | Public     | Get all topics with comments |
| GET    | `/AllTopicsByUser{userId}`       | Public     | Get all topics by a user     |
| GET    | `/AllCommentsByUserId{userId}`   | Public     | Get all comments by a user   |
| GET    | `/AllCommentsByTopicId{topicId}` | Public     | Get all comments for a topic |
| POST   | `/AddTopic`                      | Authorized | Create a new topic           |
| POST   | `/AddComment`                    | Authorized | Add a comment to a topic     |
| PUT    | `/UpdateTopic`                   | Authorized | Update a topic               |
| PUT    | `/UpdateComment`                 | Authorized | Update a comment             |
| DELETE | `/DeleteTopic{topicId}`          | Authorized | Delete a topic               |
| DELETE | `/DeleteComment{commentId}`      | Authorized | Delete a comment             |

---

## Local Development

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server or PostgreSQL
- Visual Studio 2022 / VS Code

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/AlexAmoev/TopicBoard-API-Railway.git
   cd TopicBoard-API-Railway
   ```

2. Update connection string in `Topic.API/appsettings.json`

3. Apply migrations
   ```bash
   dotnet ef database update --project Topic.Data --startup-project Topic.API
   ```

4. Run the project
   ```bash
   dotnet run --project Topic.API
   ```

5. Open Swagger UI at `https://localhost:{port}/swagger`
