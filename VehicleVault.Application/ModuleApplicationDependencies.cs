using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VehicleVault.Application.Features.UserFeature.Validations;
using VehicleVault.Application.Services.Tokens;

namespace VehicleVault.Application;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Register Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Register JWT Services
        services.AddScoped<ITokenService, TokenService>();
        
        // Register FLUENT VALIDATION
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        
        return services;
    }
}