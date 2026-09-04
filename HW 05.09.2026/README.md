# HW 05.09.2026

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

## HW 05.09.2026 - React Hook Form Category and Product Creation

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
