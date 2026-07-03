# URL Shorty API

A simple URL shortener built with **ASP.NET Core Web API**, **Entity Framework Core**, and **MySQL**.

This project allows users to convert long URLs into short codes and redirect back to the original URL.

---

# Features

- Create short URLs from long URLs
- Redirect from short URL → original URL
- Store URLs in a database (MySQL)
- Clean layered architecture (Controller → Service → Data layer)
- Async API endpoints
- API Keys for protected endpoints

---

# Tech Stack

- .NET (ASP.NET Core Web API)
- C#
- Entity Framework Core
- MySQL (via Pomelo provider)
- Rider

---
## Project Structure

```text
URLShorty
├── URLShorty              # Web API
├── Services               # Business Logic
├── ServiceContracts       # Service Interfaces & DTOs
├── Repositories           # Data Access
├── RepositoryContracts    # Repository Interfaces
├── Entities               # Domain Models
└── Tests                  # Unit Tests
```

---

## API Endpoints

### Create Short URL (Protected)

```http
POST /api/urls
```

Request:

```json
{
  "urlLong": "https://www.google.com"
}
```

Response:

```json
{
  "url": "aB12Cd",
}
```

---

### Redirect To Original URL (Public)

```http
GET /api/urls/{urlShort}
```

Example:

```http
GET /api/urls/aB12Cd
```

Response:

```http
{
  "url": "https://www.google.com"
}
```

---

### Get URL Analytics (Protected)

```http
GET /api/urls/{urlShort}/analytics
```

Example Response:

```json
{
  "urlShort": "aB12Cd",
  "urlLong": "https://www.google.com",
  "createdAt": "2026-06-13T19:54:02.500797",
  "clickCount": 42
}
```

---

### Get Analytics For All URLs (Protected)

```http
GET /api/urls
```

Example Response:

```json
[
  {
    "urlShort": "aB12Cd",
    "urlLong": "https://www.google.com",
    "createdAt": "2026-06-13T20:01:59.020072",
    "clickCount": 42
  }
]
```

---
