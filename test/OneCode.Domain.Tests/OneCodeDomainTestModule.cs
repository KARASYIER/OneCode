using OneCode.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OneCode
{
    [DependsOn(
        typeof(OneCodeEntityFrameworkCoreTestModule)
        )]
    public class OneCodeDomainTestModule : AbpModule
    {

    }
}