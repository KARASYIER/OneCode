using Localization.Resources.AbpUi;
using OneCode.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace OneCode
{
    [DependsOn(typeof(OneCodeApplicationContractsModule))]
    public class OneCodeHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OneCodeResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
