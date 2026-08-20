# HW 17.08.2026

ASP.NET Core 8 Web API with JWT authentication, roles, SQL Server, Redis
product/category caching, Docker Desktop support, and authentication features.

## Redis caching requirements implemented

### Product by ID

```http
GET /api/v1/products/{id}
```

Flow:

1. Check Redis first using `products:id:{id}`.
2. If the product exists in Redis, return the cached product.
3. If it is not cached, load it from SQL Server.
4. Store the database result in Redis.
5. Return the product to the client.

### Category by ID

```http
GET /api/v1/categories/{id}
```

Flow:

1. Check Redis first using `categories:id:{id}`.
2. If the category exists in Redis, return the cached category.
3. If it is not cached, load it from SQL Server.
4. Store the database result in Redis.
5. Return the category to the client.

Product/category cache entries expire automatically after the configured number
of minutes.

## Cache invalidation

Product caches are invalidated when a product is created, updated, or deleted.

Category caches are invalidated when a category is created, updated, or deleted.

## Test Redis

Start Redis:

```powershell
docker compose up -d redis
```

Check the Redis container:

```powershell
docker ps
```

Test Redis directly:

```powershell
docker exec -it hw17082026-redis redis-cli ping
```

Expected:

```text
PONG
```

## Test product caching

First request:

```http
GET /api/v1/products/1
```

If product `1` was not cached, the API loads it from SQL Server and stores it in:

```text
products:id:1
```

Call the same request again. The API checks Redis first and returns the cached
product.

You can also use:

```http
GET /api/v1/cache/products/1
```

This endpoint reports whether the product cache existed before and after the
request.

## Test category caching

Call:

```http
GET /api/v1/categories/1
```

The category is loaded from Redis when cached. On a cache miss it is loaded from
SQL Server and written to Redis using:

```text
categories:id:1
```

For an explicit cache test:

```http
GET /api/v1/cache/categories/1
```

## Run locally

```powershell
dotnet restore
dotnet build
dotnet run
```

Swagger is available when the application is running in Development.

## Run Redis with Docker

```powershell
docker compose up -d redis
```

## Run API and Redis with Docker

Replace the example SQL Server and SMTP environment values in
`docker-compose.yml`, then run:

```powershell
docker compose up --build
```

## Important

Do not commit real JWT keys, database passwords, SMTP passwords, or other
secrets to source control.
