# HW 28.09.2026 - ASP.NET Core MVC Controller Testing

This project contains the previous FluentValidation implementation and the required controller testing task.

## Implemented task

The task from the assignment document is: test the controller method that retrieves a category by ID.

The uploaded base project did not contain a CategoryController, so the missing category endpoint was added explicitly instead of pretending that an existing method was tested.

### Category endpoint

- `GET /api/categories/{id}`
- `CategoryController.GetById(int id)`
- Returns `200 OK` with the category when the ID exists.
- Returns `404 Not Found` when the ID does not exist.

### Automated tests

`StudentsMVC.Tests/CategoryControllerTests.cs` contains two xUnit tests:

1. `GetById_ReturnsOkWithCategory_WhenCategoryExists`
2. `GetById_ReturnsNotFound_WhenCategoryDoesNotExist`

The tests use EF Core InMemory so they do not require SQL Server.

## Run the application

```bash
dotnet restore
dotnet run --project StudentsMVC/StudentsMVC.csproj
```

## Run the tests

```bash
dotnet test
```

## Project structure

```text
HW 28.09.2026/
├── HW 28.09.2026.sln
├── StudentsMVC/
│   ├── Application/
│   ├── Controllers/
│   │   ├── CategoryController.cs
│   │   ├── HomeController.cs
│   │   └── StudentController.cs
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Student.cs
│   │   └── StudentContext.cs
│   └── StudentsMVC.csproj
└── StudentsMVC.Tests/
    ├── CategoryControllerTests.cs
    └── StudentsMVC.Tests.csproj
```

The project name is **HW 28.09.2026**.
