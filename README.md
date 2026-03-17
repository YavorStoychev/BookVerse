Това е от Workshop 2, воден от Кристиян Иванов.
Повечето неща се опитах сам до колкото мога за упражнение,
понеже нямах никаква идея за проект,
плюс че все още ми са трудни някои неща от материала и се надявам да го приемете този workshop, макар и неоригинален.
Read.Me-то е направено от chatgpt също.

#  BookVerse

**BookVerse** is a modern ASP.NET Core web application for managing and exploring books. It provides a structured and scalable architecture with separation of concerns across data, services, and presentation layers.

---

## Features

*  Browse and manage books
*  Organize books by genres
*  User authentication & authorization (ASP.NET Identity)
*  Clean layered architecture
*  Entity Framework Core with migrations
*  Supports SQL Server and SQLite

---

## Project Architecture

The solution follows a **multi-layered architecture**:

* **BookVerse.Web** – ASP.NET Core MVC frontend
* **BookVerse.Services.Core** – Business logic layer
* **BookVerse.Data** – Database context and EF Core configuration
* **BookVerse.DataModels** – Database entities
* **BookVerse.ViewModels** – Data transfer objects for the UI
* **BookVerse.GCommon** – Shared utilities and constants

---

## Technologies Used

* .NET 8
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server / SQLite
* ASP.NET Identity

---

## Getting Started

### Prerequisites

Make sure you have installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* SQL Server (optional, SQLite is supported by default)

---

###  Setup Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/BookVerse.git
   cd BookVerse
   ```

2. Apply database migrations:

   ```bash
   dotnet ef database update --project BookVerse.Data --startup-project BookVerse.Web
   ```

3. Run the application:

   ```bash
   dotnet run --project BookVerse.Web
   ```

4. Open your browser:

   ```
   https://localhost:5001
   ```

---

##  Database

The project uses **Entity Framework Core** with migrations.

* Default: SQLite
* Optional: SQL Server (configure in `appsettings.json`)

---

##  Authentication

Authentication is implemented using **ASP.NET Core Identity**, providing:

* User registration
* Login/logout
* Secure password handling

---

##  Folder Structure (Simplified)

```
BookVerse/
│
├── BookVerse.Web/          # Web layer (UI)
├── BookVerse.Services.Core/# Business logic
├── BookVerse.Data/         # EF Core & DB context
├── BookVerse.DataModels/   # Entities
├── BookVerse.ViewModels/   # DTOs / ViewModels
├── BookVerse.GCommon/      # Shared utilities
```

---

##  Future Improvements

* Add API endpoints (RESTful services)
* Implement search and filtering
* Add book reviews & ratings
* Improve UI/UX design
* Add unit and integration tests

---

##  Contributing

Contributions are welcome!

1. Fork the repo
2. Create a new branch
3. Make your changes
4. Submit a pull request

---

##  License

This project is licensed under the MIT License.

---

##  Author

Created by **Yavor Stoychev**

---
