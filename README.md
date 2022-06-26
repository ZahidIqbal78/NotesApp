Notes Management
---

---
API:
* .NET 6.0
* ASP.NET Core Web API
* In-memory database (suitable for testing, not a permanent storage. Data will not persist if the API stops/restarts)
* Entity Framework Core
* Custom authentication (JWT)
* AutoMapper
* FluentValidation

WEB:
* Angular 14

---

From the NotesApp.Web directory, execute `npm install`. Then execute `npm start` from the NotesApp.Web directory to start the web application.

From the NotesApp.API directory, executy `dotnet run` or `dotnet watch run` to run the API backend. Optional commands before `dotnet run` : `dotnet clean` and `dotnet build`.

The API backend from the root folder. In this case, the project name must be mentioned with `dotnet run` command.