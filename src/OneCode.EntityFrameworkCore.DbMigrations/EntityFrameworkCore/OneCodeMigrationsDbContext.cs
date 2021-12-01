using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See OneCodeDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class OneCodeMigrationsDbContext : AbpDbContext<OneCodeMigrationsDbContext>
    {
        public OneCodeMigrationsDbContext(DbContextOptions<OneCodeMigrationsDbContext> options) 
            : base(options)
        {
            var isFrozen = options.IsFrozen;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            /* Configure your own tables/entities inside the ConfigureOneCode method */

            builder.ConfigureOneCode();
        }
    }
}