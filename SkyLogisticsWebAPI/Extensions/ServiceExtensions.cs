using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;

namespace SkyLogisticsWebAPI.Extensions;

/*
 PURPOSE / AMAÇ:
   EN: Keeps Program.cs clean by moving DI configuration logic to extension methods.
       These are helper methods that extend IServiceCollection.
   TR: DI yapılandırma mantığını extension metodlara taşıyarak Program.cs'i temiz tutar.
       Bunlar IServiceCollection'ı genişleten yardımcı metodlardır.
 */
public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        //Database Service Setup => Dependency Injection
        // Servis kutusuna (Container) veritabanımızı ekliyoruz.
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
}