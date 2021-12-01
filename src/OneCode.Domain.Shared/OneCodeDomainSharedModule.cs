using OneCode.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;

namespace OneCode
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule)
        )]
    public class OneCodeDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            OneCodeGlobalFeatureConfigurator.Configure();
            OneCodeModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //Configure<AbpVirtualFileSystemOptions>(options =>
            //{
            //    options.FileSets.AddEmbedded<OneCodeDomainSharedModule>();
            //});

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<OneCodeResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/OneCode");

                options.DefaultResourceType = typeof(OneCodeResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("OneCode", typeof(OneCodeResource));
            });
        }
    }
}
