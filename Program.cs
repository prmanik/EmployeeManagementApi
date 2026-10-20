using EmployeeManagementApi.Filters;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/employees", (IEmployeeService employeeService) =>
{
    return Results.Ok(employeeService.GetAll());
});

app.MapGet("/api/employees/{id:int}", (IEmployeeService employeeService, int id) =>
{
    var employee = employeeService.GetById(id);
    if (employee is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(employee);
});

app.MapPost("/api/employees", (Employee employee, IEmployeeService employeeService) =>
{
    var addedEmployee = employeeService.Add(employee);
    return Results.Created($"/api/employees/{addedEmployee.Id}", addedEmployee);
}).AddEndpointFilter<ValidationFilter<Employee>>();

app.MapPut("/api/employees/{id:int}", (int id, Employee employee, IEmployeeService employeeService) =>
{
    var updated = employeeService.Update(id, employee);
    if (!updated)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
}).AddEndpointFilter<ValidationFilter<Employee>>();

app.MapDelete("/api/employees/{id:int}", (int id, IEmployeeService employeeService) =>
{
    var deleted = employeeService.Delete(id);
    if (!deleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();