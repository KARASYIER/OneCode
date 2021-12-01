using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OneCode.Data;
using Volo.Abp.DependencyInjection;

namespace OneCode.EntityFrameworkCore
{
    public class EntityFrameworkCoreOneCodeDbSchemaMigrator
        : IOneCodeDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreOneCodeDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the OneCodeMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<OneCodeMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}