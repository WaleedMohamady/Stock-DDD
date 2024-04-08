using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Interfaces;

namespace Stock.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Context
        var connectionString = configuration.GetConnectionString("StockProjectDB");
        services
            .AddDbContext<IStockDbContext, StockDbContext>(options => options.UseSqlServer(connectionString));
        #endregion

        return services;
    }
}
