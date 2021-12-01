using OneCode.Application;
using Volo.Abp.Modularity;

namespace OneCode
{
    [DependsOn(
        typeof(OneCodeApplicationModule),
        typeof(OneCodeDomainTestModule)
        )]
    public class OneCodeApplicationTestModule : AbpModule
    {

    }
}