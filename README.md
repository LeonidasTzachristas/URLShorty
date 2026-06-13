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

---

# Tech Stack

- .NET (ASP.NET Core Web API)
- C#
- Entity Framework Core
- MySQL (via Pomelo provider)
- Rider

---

# API Endpoints

### ➕ Create Short URL

```http
POST /api/urls
