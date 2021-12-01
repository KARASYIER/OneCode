using OneCode.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace OneCode.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(OneCodeEntityFrameworkCoreDbMigrationsModule),
        typeof(OneCodeApplicationContractsModule)
        )]
    public class OneCodeDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
