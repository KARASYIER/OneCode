using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace OneCode
{
    [Dependency(ReplaceServices = true)]
    public class OneCodeBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "OneCode";
    }
}
