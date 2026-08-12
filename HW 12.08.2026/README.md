# HW 10.08.2026

ASP.NET Core 8 Web API with JWT authentication, roles, password reset,
categories, products, product images, Redis caching, and Docker Desktop support.

## Redis caching

Product GET operations use Redis through `IDistributedCache`.

Cache keys:

- `products:all`
- `products:id:{id}`
- `products:category:{categoryId}`

The product cache expires automatically after the configured number of minutes.
Product create, update, and delete operations invalidate the affected cache entries.

## Docker Desktop

Install Docker Desktop for Windows and make sure it is running.

Check Docker:

```powershell
docker --version
docker compose version
```

## Start Redis only

From this project directory:

```powershell
docker compose up -d redis
```

Check the container:

```powershell
docker ps
```

Test Redis:

```powershell
docker exec -it hw10082026-redis redis-cli ping
```

Expected:

```text
PONG
```

## Run the API locally with Redis in Docker

Start Redis:

```powershell
docker compose up -d redis
```

Then:

```powershell
dotnet restore
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

The local API uses:

```text
localhost:6379
```

Test:

```text
GET /api/cache/redis
```

Expected:

```json
{
  "redis": "Redis connection works.",
  "status": "connected"
}
```

## Run API and Redis in Docker

Update the SQL Server and SMTP environment values in `docker-compose.yml`.

Then:

```powershell
docker compose up --build
```

Swagger:

```text
http://localhost:8080/swagger
```

Redis:

```text
localhost:6379
```

## Product cache test

1. Call `GET /api/Product`.
2. The first request loads products from SQL Server and stores them in Redis.
3. Call `GET /api/Product` again; the cached value is used.
4. Create, update, or delete a product.
5. The affected product and category caches are invalidated.

## Security

Do not commit real SMTP passwords, SQL Server passwords, or JWT secrets.
Use environment variables or user secrets for production credentials.
