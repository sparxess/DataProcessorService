using DataProcessorService.Application.Authorization;
using DataProcessorService.Application.Contracts.Authorization;
using DataProcessorService.Application.Contracts.Data;
using DataProcessorService.Application.Contracts.HttpLogs;
using DataProcessorService.Application.HttpLogs;
using DataProcessorService.Application.Mappers;
using DataProcessorService.Application.Values;
using DataProcessorService.Domain.Authorization;
using DataProcessorService.Domain.Data;
using DataProcessorService.Domain.HttpLogs;
using DataProcessorService.Domain.Shared.Settings;
using DataProcessorService.Domain.Shared;
using DataProcessorService.Infrastructure.Data;
using DataProcessorService.Infrastructure.EntityFrameworkCore;
using DataProcessorService.Infrastructure.HttpLogs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;

namespace DataProcessorService.HttpApi.Host.Extensions;

public static class DataProcessorServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DataProcessorServiceProperties.ConnectionStringName);
        services.AddDbContext<DataProcessorDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DataProcessorDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(DataProcessorServiceProperties.JwtSettingsSectionName).Get<JwtSettings>();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = key
            };
        });
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDataService, DataService>();
        services.AddScoped<IDataRepository, DataRepository>();
        services.AddScoped<IHttpLogService, HttpLogService>();
        services.AddScoped<IHttpLogRepository, HttpLogRepository>();
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DataProcessorServiceApplicationAutoMapperProfile));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите токен в формате: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingsSection = configuration.GetSection(DataProcessorServiceProperties.JwtSettingsSectionName);
        services.Configure<JwtSettings>(jwtSettingsSection);
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();
    }

    public static void ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Log/logs.txt", rollingInterval: RollingInterval.Infinite)
            .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog());
    }
}
