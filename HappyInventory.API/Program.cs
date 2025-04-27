using AutoMapper;
using HappyInventory.API.Configuration;
using HappyInventory.API.Seeders;
using HappyInventory.Data.Context;
using HappyInventory.Helpers.Middleware;
using HappyInventory.Models.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDynamicOrigins", policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins) 
            .AllowAnyHeader() 
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddInMemoryLogging();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("AllowDynamicOrigins");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var databaseSeeder = services.GetRequiredService<DatabaseSeeder>();
    await databaseSeeder.SeedDataAsync();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
