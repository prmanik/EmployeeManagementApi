using EmployeeManagementApi.Filters;
using EmployeeManagementApi.Handlers;
using EmployeeManagementApi.Services;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=employees.db"));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapGet("/api/employees", async (IEmployeeService employeeService) =>
{
    return Results.Ok(await employeeService.GetAllAsync());
});

app.MapGet("/api/employees/{id:int}", async (IEmployeeService employeeService, int id) =>
{
    var employee = await employeeService.GetByIdAsync(id);
    if (employee is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(employee);
});

app.MapPost("/api/employees", async (Employee employee, IEmployeeService employeeService) =>
{
    var addedEmployee = await employeeService.AddAsync(employee);
    return Results.Created($"/api/employees/{addedEmployee.Id}", addedEmployee);
}).AddEndpointFilter<ValidationFilter<Employee>>();

app.MapPut("/api/employees/{id:int}", async (int id, Employee employee, IEmployeeService employeeService) =>
{
    var updated = await employeeService.UpdateAsync(id, employee);
    if (!updated)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
}).AddEndpointFilter<ValidationFilter<Employee>>();

app.MapDelete("/api/employees/{id:int}", async (int id, IEmployeeService employeeService) =>
{
    var deleted = await employeeService.DeleteAsync(id);
    if (!deleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();