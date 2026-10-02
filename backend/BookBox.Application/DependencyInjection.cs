using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookBox.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {
        // Escanea y registra todos los validadores del ensamblado de Application
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
} 