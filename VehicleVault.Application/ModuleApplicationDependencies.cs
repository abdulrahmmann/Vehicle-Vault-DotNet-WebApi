using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VehicleVault.Application.Features.UserFeature.Validations;
using VehicleVault.Application.Services.Tokens;
using VehicleVault.Application.Services.Tokens.GeneratePrincipalJwtToken;
using VehicleVault.Application.Services.Tokens.GenerateRefreshToken;
using VehicleVault.Application.Services.Tokens.GenerateToken;

namespace VehicleVault.Application;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Register Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Register JWT Services
        services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        services.AddScoped<IGenerateRefreshTokenService, GenerateRefreshTokenService>();
        services.AddScoped<IGeneratePrincipalFromJwtTokenService, GeneratePrincipalFromJwtTokenService>();
        
        // Register FLUENT VALIDATION
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserDtoByAdminValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        
        return services;
    }
}