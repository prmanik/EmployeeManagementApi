var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/api/employees", () =>
{
    var employees = new[]
    {
        new { Id = 1, Name = "John Doe", Position = "Software Engineer" },
        new { Id = 2, Name = "Jane Smith", Position = "Project Manager" },
        new { Id = 3, Name = "Bob Johnson", Position = "QA Analyst" }
    };
    return Results.Ok(employees);
});

app.MapGet("api/employees/{id:int}", (int id) =>
{
    var employees = new[]
    {
        new { Id = 1, Name = "John Doe", Position = "Software Engineer" },
        new { Id = 2, Name = "Jane Smith", Position = "Project Manager" },
        new { Id = 3, Name = "Bob Johnson", Position = "QA Analyst" }
    };

    var employee = employees.FirstOrDefault(e => e.Id == id);
    if (employee is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(employee);
});
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
