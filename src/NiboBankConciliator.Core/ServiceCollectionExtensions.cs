using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NiboBankConciliator.Core.Services;

namespace NiboBankConciliator.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all type of services: logical services, repository services, DbContext, ...
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddAllServices(this IServiceCollection serviceCollection, string bankConciliatorConnectionString)
        {
            serviceCollection.AddLogicalServices();
            serviceCollection.AddRepositories(bankConciliatorConnectionString);
            return serviceCollection;
        }

        /// <summary>
        /// Add logical services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddLogicalServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBankReconciliationService, BankReconciliationService>();
        }

        /// <summary>
        /// Add repository services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddRepositories(this IServiceCollection serviceCollection, string bankConciliatorConnectionString)
        {
            serviceCollection.AddBankConciliatorRepository(bankConciliatorConnectionString);
        }

        /// <summary>
        /// Add BankConciliatorRepository and BankConciliatorDbContext
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddBankConciliatorRepository(this IServiceCollection serviceCollection, string bankConciliatorConnectionString)
        {
            serviceCollection.AddDbContext<BankConciliatorDbContext>(options => options.UseSqlServer(bankConciliatorConnectionString));
            serviceCollection.AddScoped<IBankConciliatorRepository, BankConciliatorRepository>();
        }
    }
}
