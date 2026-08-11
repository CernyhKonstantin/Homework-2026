# HW 10.08.2026

ASP.NET Core 8 Web API with:

- JWT access tokens (15 minutes)
- Refresh tokens in HttpOnly cookies
- User roles: User, Moderator, Admin
- Automatic first-admin seeding
- Admin-only user/role management
- Password reset by email
- Category hierarchy
- Product management with up to 5 images
- Swagger

## First setup

1. Open the project directory in Visual Studio or a terminal.
2. Update `appsettings.json`.
3. Set the SQL Server name in `ConnectionStrings`.
4. Set a strong JWT key.
5. Configure SMTP under `Email`.
6. Change the seeded admin email/password.
7. Run:

```powershell
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

The application creates the first admin if no Admin exists.

## Important

The sample SMTP and admin credentials in `appsettings.json` are placeholders and must be changed before real use.
