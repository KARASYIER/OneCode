using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace OneCode.EntityFrameworkCore
{
    [DependsOn(
        typeof(OneCodeEntityFrameworkCoreModule)
        )]
    public class OneCodeEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OneCodeMigrationsDbContext>();
        }
    }
}
