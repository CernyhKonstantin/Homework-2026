# GitHub Setup Guide

## Files that are intentionally not committed

The repository ignores Visual Studio files, .NET build output, Node modules, Vite build output, local environment files, uploaded files, logs, and ZIP archives.

## Configuration

`appsettings.json` contains development-safe placeholder values. For real deployments, use environment variables, user secrets, or a secure secret store instead of committing credentials.

Copy `appsettings.example.json` as a reference when creating a local configuration.

## Run the backend

```powershell
dotnet restore
dotnet ef database update
dotnet run
```

Swagger is available at:

```text
https://localhost:7048/swagger
```

## Run the React frontend

```powershell
cd frontend
npm install
npm run dev
```

## Git commands

```powershell
git init
git add .
git status
git commit -m "HW 09.09.2026 - multiple delivery addresses"
git branch -M main
git remote add origin YOUR_GITHUB_REPOSITORY_URL
git push -u origin main
```

Before pushing, always run `git status` and verify that no real passwords, API keys, JWT secrets, SMTP credentials, or connection strings containing credentials are staged.
