﻿using HappyInventory.API.Seeders;
using HappyInventory.Data.Repositories;
using HappyInventory.Models.Models.Identity;
using HappyInventory.Models.Models.Warehouses;
using HappyInventory.Services.CountryService.CounrtySeeder;
using HappyInventory.Services.JwtService;
using HappyInventory.Services.JwtService.Interface;
using HappyInventory.Services.LogService;
using HappyInventory.Services.UserService;
using HappyInventory.Services.UserService.SeedData;
using HappyInventory.Services.WarehouseService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HappyInventory.API.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                    .FromAssemblyOf<IRepository<Warehouse>>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserSeeder, UserSeeder>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IWarehouseService, WarehouseService>() ;
            services.AddScoped<ICountrySeeder, CountrySeeder>();
            services.AddScoped<DatabaseSeeder>();

            services.AddSingleton<ILogsService, LogsService>();

            return services;
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"]
                    };
                });
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token as `Bearer {your token}`"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void AddInMemoryLogging(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }

    }
}
