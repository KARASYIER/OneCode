using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace OneCode
{
    [DependsOn(
        typeof(OneCodeDomainSharedModule),
        typeof(AbpObjectExtendingModule)
    )]
    public class OneCodeApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            OneCodeDtoExtensions.Configure();
        }
    }
}
