using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OneCode.Application;
using OneCode.EntityFrameworkCore;
using OneCode.Extenstion;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Abp.VirtualFileSystem;

namespace OneCode
{
    [DependsOn(
        typeof(OneCodeHttpApiModule),
        typeof(OneCodeApplicationModule),
        typeof(OneCodeEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
    )]
    public class OneCodeHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            //ConfigureAuthentication(context, configuration);
            //ConfigureLocalization();
            //ConfigureCache(configuration);
            //ConfigureVirtualFileSystem(context);
            //ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);

            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
            });
            context.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Clear();
            });
            ConfigureOneCodeSetting(context, configuration);
        }

        #region Private Method
        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "OneCode:"; });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<OneCodeDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}OneCode.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<OneCodeDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}OneCode.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<OneCodeApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}OneCode.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<OneCodeApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}OneCode.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {

                options.ConventionalControllers.Create(typeof(OneCodeApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "onecode";
                    opt.UrlActionNameNormalizer = (lizer) => { return lizer.ActionNameInUrl.ToKebabCase(); };
                    opt.UrlControllerNameNormalizer = (lizer) => { return lizer.ControllerName.ToKebabCase(); };
                });
            });


        }


        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "OneCode";
                });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "OneCode API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            });
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "OneCode-Protection-Keys");
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private void ConfigureOneCodeSetting(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.Configure<OneCodeSettingOptions>(configuration.GetSection(OneCodeSettingOptions.OneCodeSetting));
        }

        #endregion

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseOneCodeExceptionHandler();
            //app.UseExceptionHandler();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Upload")),
                RequestPath = new PathString("/Upload")
            });

            ;

            //app.UseAbpRequestLocalization();

            //if (!env.IsDevelopment())
            //{
            //    app.UseErrorPage();
            //}

            //app.UseCorrelationId();
            //app.UseVirtualFiles();

           

            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            //app.UseOneCodeLogging();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "自在一店一码 API"); });

            //app.UseAuditing();
            //app.UseAbpSerilogEnrichers();

            app.UseUnitOfWork();

            app.UseConfiguredEndpoints(options=> {
                options.MapControllerRoute("default", "api/onecode/[controller]/[action]");
                options.MapControllers();
            });
            
        }
    }
}