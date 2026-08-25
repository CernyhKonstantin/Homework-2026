# HW 24.08.2026

ASP.NET Core 8 Web API with JWT authentication, Redis product/category caching,
and RabbitMQ user-registration messaging.

## RabbitMQ requirements implemented

### 1. RabbitMQ connection settings

RabbitMQ connection settings are no longer hard-coded in the application.

The API reads them from:

```text
appsettings.json
```

The dedicated `RabbitMqSettings` class maps:

- HostName
- Port
- UserName
- Password
- VirtualHost
- UserQueue
- Durable
- AutoDelete

The `RabbitMqReader` is a separate .NET 8 console application with its own:

```text
RabbitMqReader/appsettings.json
```

This keeps the reader independent from the API configuration.

### 2. Registration publishes to Users queue

After a user is successfully persisted by the registration flow, the API publishes
a JSON message to the RabbitMQ queue:

```text
Users
```

Example message:

```json
{
  "UserId": 123,
  "Email": "user@example.com",
  "RegisteredAtUtc": "2026-08-19T12:00:00Z"
}
```

The message is marked persistent and the `Users` queue is durable.

### 3. RabbitMqReader consumes Users

The `RabbitMqReader` project consumes the `Users` queue with manual acknowledgements.

A successful message is acknowledged with:

```text
BasicAck
```

RabbitMQ then removes it from the queue.

A failed message is rejected with requeue enabled, so it remains available for
another processing attempt.

## RabbitMQ Web Management UI

The Docker Compose configuration exposes:

```text
http://localhost:15672
```

Default development credentials:

```text
Username: guest
Password: guest
```

Open the **Queues and Streams** page and select:

```text
Users
```

You can see:

- message count
- ready messages
- unacknowledged messages
- consumers

## Start RabbitMQ with Docker Desktop

Make sure Docker Desktop is running.

From the project directory:

```powershell
docker compose up -d rabbitmq
```

Check:

```powershell
docker ps
```

Open:

```text
http://localhost:15672
```

## Run the API

```powershell
dotnet restore
dotnet build
dotnet run
```

If RabbitMQ runs locally through Docker, the API uses:

```text
localhost:5672
```

## Run RabbitMqReader

Open a second PowerShell window:

```powershell
cd "RabbitMqReader"
dotnet restore
dotnet run
```

Expected output:

```text
RabbitMqReader is listening on queue 'Users'.
Create users in the API and watch them appear here.
Press Ctrl+C to stop.
```

## Test the assignment

1. Start Docker Desktop.
2. Start RabbitMQ:
   ```powershell
   docker compose up -d rabbitmq
   ```
3. Open RabbitMQ Management:
   ```text
   http://localhost:15672
   ```
4. Open the `Users` queue.
5. Start the API.
6. Start `RabbitMqReader`.
7. Register a new user through the web/API registration form.
8. The API writes the registration message to `Users`.
9. `RabbitMqReader` receives the message.
10. The reader acknowledges the message.
11. The message disappears from the queue.

For a visual proof, take screenshots of:

- RabbitMQ Management UI with the `Users` queue.
- RabbitMqReader console showing `Received user: ...`.
- RabbitMQ `Users` queue after processing showing `Ready = 0`.

## Docker Compose

The compose file now contains:

- Redis
- RabbitMQ with Management UI
- API

RabbitMQ ports:

```text
5672  - AMQP
15672 - Management UI
```

Redis port:

```text
6379
```

## Security

The default `guest/guest` credentials are intended for local development only.
Use environment variables or Docker secrets for production credentials.

## Orders and RabbitMQ

The project now implements asynchronous order processing through RabbitMQ.

### Create an order

```http
POST /api/v1/orders
Authorization: Bearer <access-token>
Content-Type: application/json
```

Example:

```json
{
  "paid": false,
  "products": [
    { "productId": 1, "count": 2 },
    { "productId": 3, "count": 1 }
  ]
}
```

The API takes the authenticated user ID from the JWT and publishes the order to the `Orders` RabbitMQ queue.

### Order consumer

`OrderQueueConsumer` runs as a hosted background service. It reads the `Orders` queue and:

1. Validates the user and requested products.
2. Checks that every requested product is active and has enough stock.
3. If stock is sufficient, creates `Orders` and `OrderDetails` in SQL Server and decreases stock inside a transaction.
4. Sends an email containing product names, prices, quantities and the total price.
5. If stock is insufficient, sends an email explaining which products are waiting.
6. Acknowledges successfully processed RabbitMQ messages.

### Database migration

The `Migrations/20260825000000_AddOrders.cs` migration creates:

- `Orders`
- `OrderDetails`

Apply it with:

```powershell
dotnet ef database update
```

If the database does not exist yet, make sure the SQL Server connection string is configured first.

### RabbitMQ

The queue name is configured in `appsettings.json`:

```json
"OrdersQueue": "Orders"
```

Docker exposes RabbitMQ AMQP on `5672` and the management UI on `15672`.
