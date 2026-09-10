# HW 09.09.2026

ASP.NET Core 8 Web API with JWT authentication, Redis caching, RabbitMQ messaging, SQL Server orders, email notifications, and MongoDB product feedback/questions.

## MongoDB product feedback and questions

This homework adds MongoDB to the main ASP.NET Core API project. The official `MongoDB.Driver` package is referenced in the main `.csproj`.

The application reads MongoDB settings from `appsettings.json`:

```json
"MongoDb": {
  "ConnectionString": "mongodb://admin:querty@localhost:27017",
  "DatabaseName": "ShopDb",
  "FeedbackCollectionName": "ProductFeedback"
}
```

The MongoDB document is designed as `ProductFeedback` and contains:

- `Id`
- `ProductId`
- `UserId`
- `UserEmail`
- `Type` (`review` or `question`)
- `Message`
- `Rating` (1-5 for reviews, null for questions)
- `CreatedAt`

## Endpoints

### Add a review

```http
POST /api/v1/products/1/feedback
Authorization: Bearer YOUR_ACCESS_TOKEN
Content-Type: application/json
```

```json
{
  "type": "review",
  "message": "Great product and fast delivery.",
  "rating": 5
}
```

### Add a product question

```http
POST /api/v1/products/1/feedback
Authorization: Bearer YOUR_ACCESS_TOKEN
Content-Type: application/json
```

```json
{
  "type": "question",
  "message": "Is this product available in another size?",
  "rating": null
}
```

### Read feedback/questions for a product

```http
GET /api/v1/products/1/feedback
```

The POST endpoint requires an authenticated user. The API gets `UserId` and email from the JWT instead of trusting the client to provide them.

## MongoDB Docker setup

Docker Compose now starts:

- SQL Server connection for the API
- Redis
- RabbitMQ + Management UI
- MongoDB

Start MongoDB with:

```powershell
docker compose up -d mongodb
```

Or start the complete infrastructure:

```powershell
docker compose up -d
```

MongoDB is available at:

```text
localhost:27017
```

Credentials for local development:

```text
Username: admin
Password: querty
Database: ShopDb
```

The API uses the Docker hostname `mongodb` when the API itself runs inside Docker.

## Install package manually

From the API project directory:

```powershell
dotnet add package MongoDB.Driver --version 3.11.1
```

## Run

```powershell
dotnet restore
dotnet build
dotnet run
```

Open Swagger in development and authorize with a JWT token. Then use the product feedback endpoint to create a review or question and the GET endpoint to verify the stored MongoDB documents.

## Important

The `admin/querty` MongoDB credentials are development credentials only. Use secrets/environment variables for production.

## HW 09.09.2026 - React Hook Form Category and Product Creation

The project now includes a complete React frontend in `frontend/`.

### Assignment
- Create categories using `react-hook-form`.
- Create products using `react-hook-form`.
- Product creation uses `FormData` because the API supports up to 5 product images.
- Data is sent to the ASP.NET Core API and persisted to SQL Server through Entity Framework Core.
- The frontend displays the categories and products returned from the database.
- Category and product creation requires an Admin or Moderator JWT.

### Run the backend
```powershell
dotnet restore
dotnet ef database update
dotnet run
```

Update the SQL Server connection string in `appsettings.json` if necessary.

### Run the React frontend
```powershell
cd frontend
npm install
npm run dev
```

The Vite frontend uses `https://localhost:7048` as the default API URL. If your ASP.NET Core HTTPS port is different, change `API_URL` and `AUTH_URL` in `frontend/src/App.jsx`.

### Default seeded administrator
- Email: `admin@example.com`
- Password: `ChangeMe123!`

Change the default password for any real deployment.

## CQRS + MediatR

This version introduces CQRS with MediatR for the requested category and product operations.

### Commands

1. `CreateCategoryCommand` - creates a category through MediatR.
2. `CreateProductCommand` - creates a product and optional images through MediatR.

### Queries

1. `GetCategoryBySlugQuery` - returns a category by its slug.
2. `GetCategoryByIdQuery` - returns a category by its numeric ID.

The API controller sends commands and queries through `IMediator` instead of directly executing the corresponding create/read operation.

### Category endpoints

```http
POST /api/v1/categories
GET  /api/v1/categories/{slug}
GET  /api/v1/categories/{id}
```

Example slug request:

```http
GET /api/v1/categories/electronics
```

Example ID request:

```http
GET /api/v1/categories/1
```

### Product endpoint

```http
POST /api/v1/products
Content-Type: multipart/form-data
```

The product creation request is handled by `CreateProductCommand` and `CreateProductCommandHandler`.

### CQRS structure

```text
CQRS/
├── Commands/
│   ├── Categories/
│   │   ├── CreateCategoryCommand.cs
│   │   └── CreateCategoryCommandHandler.cs
│   └── Products/
│       ├── CreateProductCommand.cs
│       └── CreateProductCommandHandler.cs
└── Queries/
    └── Categories/
        ├── GetCategoryByIdQuery.cs
        ├── GetCategoryByIdQueryHandler.cs
        ├── GetCategoryBySlugQuery.cs
        └── GetCategoryBySlugQueryHandler.cs
```

MediatR is registered with the main application assembly in `Program.cs`.

## HW 09.09.2026 - Multiple User Delivery Addresses

This version adds the ability for an authenticated user to attach multiple delivery addresses to their own account.

### Features

- `UserAddress` entity stored in SQL Server.
- One user can have many delivery addresses.
- Each address is linked to the authenticated user's `UserId`.
- Users can create, list, read, and delete their own addresses.
- Users cannot access another user's addresses because every repository lookup is filtered by the authenticated `UserId`.
- The React frontend contains an address form implemented with `react-hook-form` and displays all addresses belonging to the logged-in user.
- The API controller explicitly accepts and forwards `CancellationToken` for every asynchronous address endpoint.
- Entity Framework Core passes the token to database operations via `ToListAsync`, `FirstOrDefaultAsync`, and `SaveChangesAsync`.

### Address API

All address endpoints require a valid JWT access token.

```http
GET    /api/v1/users/me/addresses
GET    /api/v1/users/me/addresses/{addressId}
POST   /api/v1/users/me/addresses
DELETE /api/v1/users/me/addresses/{addressId}
```

Example request:

```json
{
  "label": "Home",
  "recipientName": "John Doe",
  "country": "Germany",
  "city": "Munich",
  "postalCode": "80331",
  "street": "Main Street",
  "houseNumber": "12a",
  "apartment": "Apartment 4",
  "phone": "+49 170 1234567"
}
```

### CancellationToken requirement

The controller methods intentionally use the ASP.NET Core cancellation token provided by the request pipeline:

```csharp
[HttpPost]
public async Task<ActionResult<UserAddressReadDto>> Create(
    [FromBody] CreateUserAddressDto dto,
    CancellationToken cancellationToken)
{
    // ...
    var address = await _service.CreateAsync(userId, dto, cancellationToken);
    // ...
}
```

The token is propagated through the service and repository layers until EF Core executes the database operation. This allows an abandoned HTTP request to cancel the underlying asynchronous database work when possible.

### Database migration

Apply the new migration after restoring the project:

```powershell
dotnet restore
dotnet ef database update
```

The migration creates the `UserAddresses` table with a foreign key to `Users` and cascade delete behavior.

### React frontend

```powershell
cd frontend
npm install
npm run dev
```

After logging in, use **Add Delivery Address** to attach multiple addresses to the account. The **My Delivery Addresses** section loads the saved addresses from SQL Server and allows the current user to remove them.


## GitHub

This repository is GitHub-ready. See `GITHUB_SETUP.md` for the recommended Git workflow and configuration guidance.
