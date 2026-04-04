# 📚 Library Management System API
A professional Backend API built with **ASP.NET Core** following modern architectural patterns and best practices.

## 🛠️ Tech Stack & Tools
* **Framework:** .NET Core Web API
* **Database:** SQL Server (Entity Framework Core)
* **Patterns:** Repository Pattern, CQRS with **MediatR**
* **Security:** JWT Authentication & ASP.NET Core Identity
* **Tools:** Swagger UI, AutoMapper, Postman

## 🌟 Key Features
* **Advanced Architecture:** Decoupled logic using MediatR for clean and maintainable code.
* **Authentication & Authorization:** Secure access using JWT tokens with Role-based access control (Admin/User).
* **Core Modules:** Complete Management for Books, Categories, Borrowing records, and User Reviews.
* **Image Handling:** Implemented functionality for uploading and managing book covers.
* **Data Transfer:** Structured communication using DTOs to ensure data integrity.

## 🏗️ Project Structure
The project follows a **Separation of Concerns** approach:
- **Controllers:** Handling HTTP requests.
- **Application:** Logic, Commands, Queries, and DTOs.
- **Domain:** Entities and core business rules.
- **Infrastructure:** Persistence (SQL Server) and Repository implementations.

## 🚀 How to Run
1. Clone the repo: `git clone [REPO LINK]`
2. Update `appsettings.json` with your Connection String.
3. Run `Update-Database` in Package Manager Console.
4. Press F5 to run the API and explore via Swagger.
