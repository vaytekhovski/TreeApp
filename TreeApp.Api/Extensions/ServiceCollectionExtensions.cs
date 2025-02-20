using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TreeApp.Api.Configuration;
using TreeApp.Application.Interfaces;
using TreeApp.Application.Services;
using TreeApp.Infrastructure.Data;

namespace TreeApp.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            options.UseNpgsql(dbSettings.ConnectionString);
        });

        services.AddScoped<ITreeRepository, TreeRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<INodeValidator, NodeValidator>();
        services.AddScoped<ITreeService, TreeService>();
        services.AddScoped<IJournalService, JournalService>();
        services.AddControllers();

        return services;
    }
}