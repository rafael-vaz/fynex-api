# Fynex API 💸

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-6DB33F)
![MySQL](https://img.shields.io/badge/MySQL-8.x-4479A1?logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT-black)

REST API for personal expense management, with JWT authentication, user management, expense CRUD, and report generation in **Excel** and **PDF** formats.

## 📌 Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Architecture and Organization](#architecture-and-organization)
- [DDD Principles](#ddd-principles)
- [Features](#features)
- [API Routes](#api-routes)
- [Request Models](#request-models)
- [Installation and Run](#installation-and-run)
- [Configuration](#configuration)
- [Database and Migrations](#database-and-migrations)
- [Tests](#tests)
- [Main Libraries](#main-libraries)

<a id="overview"></a>
## 🧾 Overview

**Fynex** is a backend API focused on personal finance. The project provides:

- User registration and authentication;
- Profile management (update, password change, and account deletion);
- Full expense registration and management;
- Monthly expense report export in **.xlsx** and **.pdf** formats;
- Role-based access control (including an admin route for reports).


<a id="technologies-used"></a>
## 🛠 Technologies Used

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core 9**
- **Pomelo.EntityFrameworkCore.MySql** (MySQL)
- **JWT Bearer Authentication**
- **Swagger / OpenAPI**
- **AutoMapper**
- **FluentValidation**
- **ClosedXML** (Excel generation)
- **PDFsharp-MigraDoc** (PDF generation)
- **xUnit + Shouldly** (testing)


<a id="architecture-and-organization"></a>
## 🧱 Architecture and Organization

Structure based on separation by layers/projects:

```text
src/
 ├─ Fynex.Api              # Presentation layer (controllers, middleware, auth, swagger)
 ├─ Fynex.Application      # Use cases, validations, and application rules
 ├─ Fynex.Domain           # Entities, contracts, and domain rules
 ├─ Fynex.Infrastructure   # Persistence, security, tokens, and repository implementations
 ├─ Fynex.Communication    # Request/response DTOs and contract enums
 └─ Fynex.Exception        # Exception handling

tests/
 ├─ WebApi.Test
 ├─ UseCases.Test
 ├─ Validators.Test
 └─ CommonTestUtilities
```

<a id="ddd-principles"></a>
## 🧠 DDD Principles

The project follows **Domain-Driven Design (DDD)** principles to organize responsibilities and keep the domain decoupled from infrastructure details:

- **Domain**: centralizes entities, enums, and repository/domain service contracts;
- **Application**: orchestrates use cases and application rules;
- **Infrastructure**: implements persistence, security, and technical integrations;
- **Api**: entry layer (HTTP), responsible for exposing endpoints and composing the application.

This organization improves maintainability, evolution, and testability.

<a id="features"></a>
## ✅ Features

### Users
- User registration;
- Login with JWT token response;
- Authenticated profile retrieval;
- Profile update;
- Password change;
- Account deletion.

### Expenses
- Expense creation;
- Authenticated user expense listing;
- Expense retrieval by ID;
- Expense update;
- Expense deletion.

### Reports
- Monthly report generation in **Excel**;
- Monthly report generation in **PDF**;
- Restricted access for **administrator** role.

<a id="api-routes"></a>
## 🌐 API Routes

Base prefix: `/api`

| Resource | Method | Route | Authentication | Description |
|---|---|---|---|---|
| Users | `POST` | `/users` | No | User registration |
| Users | `GET` | `/users` | Yes | Returns authenticated user profile |
| Users | `PUT` | `/users` | Yes | Updates profile |
| Users | `PUT` | `/users/change-password` | Yes | Changes password |
| Users | `DELETE` | `/users` | Yes | Deletes account |
| Login | `POST` | `/login` | No | Authentication and token retrieval |
| Expenses | `POST` | `/expenses` | Yes | Creates expense |
| Expenses | `GET` | `/expenses` | Yes | Lists expenses |
| Expenses | `GET` | `/expenses/{id}` | Yes | Gets expense by ID |
| Expenses | `PUT` | `/expenses/{id}` | Yes | Updates expense |
| Expenses | `DELETE` | `/expenses/{id}` | Yes | Deletes expense |
| Reports | `GET` | `/report/excel?month=YYYY-MM-DD` | Yes (admin) | Exports Excel report |
| Reports | `GET` | `/report/pdf?month=YYYY-MM-DD` | Yes (admin) | Exports PDF report |

> For protected routes, send the JWT token in the `Authorization` header.

<a id="request-models"></a>
## 📥 Request Models

### User Registration
```json
{
  "name": "Maria Silva",
  "email": "maria@email.com",
  "password": "Senha@123"
}
```

### Login
```json
{
  "email": "maria@email.com",
  "password": "Senha@123"
}
```

### Expense Create/Update
```json
{
  "title": "Supermercado",
  "description": "Compras do mês",
  "date": "2026-04-22T18:30:00",
  "amount": 320.50,
  "paymentType": 1,
  "tags": [1, 3, 9]
}
```

### Password Change
```json
{
  "password": "SenhaAtual@123",
  "newPassword": "NovaSenha@456"
}
```

Enums used in `RequestExpenseJson`:

- **paymentType**: `Cash(0)`, `CreditCard(1)`, `DebitCard(2)`, `ElectronicTransfer(3)`
- **tags**: `Health(0)`, `Essential(1)`, `Variable(2)`, `Fixed(3)`, `Personal(4)`, `Emergency(5)`, `Investment(6)`, `Leisure(7)`, `Education(8)`, `Transportation(9)`


<a id="installation-and-run"></a>
## 🚀 Installation and Run

### Prerequisites

- .NET SDK 9
- MySQL 8+ (for local development environment)

### Steps

1. Clone the repository:

```bash
git clone <repository-url>
cd Fynex
```

2. Restore packages:

```bash
dotnet restore
```

3. Run the API:

```bash
dotnet run --project src/Fynex.Api/Fynex.Api.csproj
```

4. Open Swagger (Development environment):

```text
https://localhost:<porta>/swagger
```

<a id="configuration"></a>
## ⚙️ Configuration

Main configuration files:

- `src/Fynex.Api/appsettings.json`
- `src/Fynex.Api/appsettings.Development.json`
- `src/Fynex.Api/appsettings.Test.json`

### Example (Development)

```json
{
  "ConnectionStrings": {
    "Connection": "Server=localhost;Database=fynexdb;Uid=fynexuser;Pwd=fynexpassword;"
  },
  "Settings": {
    "Jwt": {
      "SigningKey": "<sua-chave>",
      "ExpiresMinutes": 1000
    }
  }
}
```

<a id="database-and-migrations"></a>
## 🗃 Database and Migrations

The project uses EF Core with MySQL via Pomelo.

Useful command:

```bash
dotnet ef database update --project src/Fynex.Infrastructure/Fynex.Infrastructure.csproj --startup-project src/Fynex.Api/Fynex.Api.csproj
```

> The application also runs automatic migration at startup when it is **not** in the test environment (`InMemoryTest = false`).


<a id="tests"></a>
## 🧪 Tests

Run all tests:

```bash
dotnet test
```

Run only API tests:

```bash
dotnet test tests/WebApi.Test/WebApi.Test.csproj
```

<a id="main-libraries"></a>
## 📚 Main Libraries

- `Swashbuckle.AspNetCore` → OpenAPI/Swagger documentation
- `Microsoft.AspNetCore.Authentication.JwtBearer` → JWT authentication
- `Microsoft.EntityFrameworkCore` / `Pomelo.EntityFrameworkCore.MySql` → MySQL persistence
- `AutoMapper` → entity/DTO mapping
- `FluentValidation` → input validation
- `ClosedXML` → Excel file generation
- `PDFsharp-MigraDoc` → PDF file generation
- `BCrypt.Net-Next` → password hashing
