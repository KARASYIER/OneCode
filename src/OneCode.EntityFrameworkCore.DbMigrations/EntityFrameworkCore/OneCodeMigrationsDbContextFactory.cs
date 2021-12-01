using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OneCode.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class OneCodeMigrationsDbContextFactory : IDesignTimeDbContextFactory<OneCodeMigrationsDbContext>
    {
        public OneCodeMigrationsDbContext CreateDbContext(string[] args)
        {
            OneCodeEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<OneCodeMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new OneCodeMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OneCode.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
