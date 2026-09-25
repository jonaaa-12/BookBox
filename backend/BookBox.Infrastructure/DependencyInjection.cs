using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookBox.Application.Interfaces;
using BookBox.Infrastructure.Data;
using BookBox.Infrastructure.Repositories;

namespace BookBox.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        // 1. Configurar la conexión a PostgreSQL
        services.AddDbContext<BookBoxDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. Registrar el repositorio (Inversión de Dependencias)
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
} 