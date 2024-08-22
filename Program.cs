// using TaskManagementApi.Models; 
// using TaskManagementApi.Repositories;
// using Microsoft.Extensions.Options;

// var builder = WebApplication.CreateBuilder(args);

// // Allowed origins
// string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// // Configure CORS
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//         policy  =>
//         {
//             policy.WithOrigins("http://localhost:3000")
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//         });
// });
// // Add services to the container.
// builder.Services.Configure<DatabaseSettings>(
//     builder.Configuration.GetSection("DatabaseSettings"));

// // Register the ITaskRepository service with its implementation
// builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();
// // Use CORS
// app.UseCors(MyAllowSpecificOrigins);

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();
using TaskManagementApi.Models;
using TaskManagementApi.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Allowed origins
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Register the ITaskRepository service with its implementation
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve static files from wwwroot
app.UseStaticFiles();

// Use routing
app.UseRouting();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Serve the React app for non-API routes
app.MapFallbackToFile("/ClientApp/index.html");

app.Run();
