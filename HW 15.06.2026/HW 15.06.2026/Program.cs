var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Task 2: Root route
app.MapGet("/", () => "Welcome to MyHomeWeb!");

// Task 3: Route with parameter
app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");

// Task 4: JSON endpoint
app.MapGet("/api/status", () => new
{
    message = "MyHomeWeb is running",
    time = DateTime.Now
});

app.Run();