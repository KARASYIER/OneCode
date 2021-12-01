using OneCode.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace OneCode
{
    [DependsOn(
        typeof(OneCodeDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule)
    )]
    public class OneCodeDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }
    }
}
