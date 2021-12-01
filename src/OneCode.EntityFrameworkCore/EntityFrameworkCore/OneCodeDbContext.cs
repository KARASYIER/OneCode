using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See OneCodeMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class OneCodeDbContext : AbpDbContext<OneCodeDbContext>
    {

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<Saler> Salers { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BizImage> BizImages { get; set; }

        public DbSet<ShopTag> ShopTags { get; set; }

        public DbSet<ShopProduct> ShopProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Draw> Draws { get; set; }

        public DbSet<CommisionRecord> CommisionRecords { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside OneCodeDbContextModelCreatingExtensions.ConfigureOneCode
         */

        public OneCodeDbContext(DbContextOptions<OneCodeDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */



            /* Configure your own tables/entities inside the ConfigureOneCode method */
            builder.ConfigureOneCode();
        }
    }
}
