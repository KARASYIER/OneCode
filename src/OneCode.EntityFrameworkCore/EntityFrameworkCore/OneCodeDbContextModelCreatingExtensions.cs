using Microsoft.EntityFrameworkCore;
using OneCode.Domain;
using OneCode.EnumTypes;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace OneCode.EntityFrameworkCore
{
    public static class OneCodeDbContextModelCreatingExtensions
    {
        public static void ConfigureOneCode(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AdminUser>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "AdminUsers", OneCodeConsts.DbSchema);

                b.Property(p => p.Mobile).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Mobile);
                b.Property(p => p.Password).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Password);
                b.Property(p => p.RealName).HasMaxLength(OneCodeConsts.DbLength_Name);

                b.ConfigureByConvention();
            });

            builder.Entity<Saler>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Salers", OneCodeConsts.DbSchema);

                b.Property(p => p.Mobile).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Mobile);
                b.Property(p => p.Password).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Password);
                b.Property(p => p.Name).HasMaxLength(OneCodeConsts.DbLength_Name);
                b.Property(p => p.QRCodeUrl).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.Openid).HasMaxLength(OneCodeConsts.DbLength_Openid);
                b.Property(p => p.HeadImgUrl).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.Nickname).HasMaxLength(OneCodeConsts.DbLength_Name);

                b.ConfigureByConvention();

            });

            builder.Entity<Shop>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Shops", OneCodeConsts.DbSchema);

                b.Property(p => p.Name).IsRequired().HasMaxLength(OneCodeConsts.DbLength_ShopName);
                b.Property(p => p.Summary).HasMaxLength(OneCodeConsts.DbLength_Summary);
                b.Property(p => p.Description).HasMaxLength(OneCodeConsts.DbLength_Description);
                b.Property(p => p.Logo).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.Kv).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.Telephone).HasMaxLength(OneCodeConsts.DbLength_Password);
                b.Property(p => p.Address).HasMaxLength(OneCodeConsts.DbLength_Address);
                b.Property(p => p.Longitude).HasMaxLength(OneCodeConsts.DbLength_Password);
                b.Property(p => p.Latitude).HasMaxLength(OneCodeConsts.DbLength_Password);
                b.Property(p => p.OwnerName).HasMaxLength(OneCodeConsts.DbLength_Name);
                b.Property(p => p.Template).HasDefaultValue(ShopTemplateTypeEnum.Default);
                b.Property(p => p.Status).HasDefaultValue(ShopStatusEnum.Openning);
                b.Property(p => p.QRCodeUrl).HasMaxLength(OneCodeConsts.DbLength_Url);


                b.Property(p => p.CommisionAvailable).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                b.Property(p => p.CommisionApplying).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                b.Property(p => p.CommisionDoing).HasColumnType("decimal(18,2)").HasDefaultValue(0);


                b.ConfigureByConvention();


                b.HasMany(p => p.Salers).WithOne(p => p.Shop).HasForeignKey(p => p.ShopId).OnDelete(DeleteBehavior.Cascade);
            });

            #region BizImage
            builder.Entity<BizImage>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "BizImages", OneCodeConsts.DbSchema);
                b.Property(p => p.Url).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.ThumbUrl).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.OriginalUrl).HasMaxLength(OneCodeConsts.DbLength_Url);

                b.ConfigureByConvention();
            });
            #endregion

            #region Tag
            builder.Entity<Tag>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Tags", OneCodeConsts.DbSchema);
                b.Property(p => p.Name).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Name);
                b.ConfigureByConvention();
            });
            #endregion


            builder.Entity<ShopTag>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "ShopTags", OneCodeConsts.DbSchema);

                b.ConfigureByConvention();

                //ShopTag表设置复合主键
                b.HasKey((st) => new { st.ShopId, st.TagId });

                b.HasOne<Shop>(p => p.Shop).WithMany(p => p.ShopTags).HasForeignKey(p => p.ShopId).OnDelete(DeleteBehavior.Cascade);
                b.HasOne<Tag>(p => p.Tag).WithMany(p => p.ShopTags).HasForeignKey(p => p.TagId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Product>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Products", OneCodeConsts.DbSchema);

                b.Property(p => p.Title).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Title);
                b.Property(p => p.Summary).HasMaxLength(OneCodeConsts.DbLength_Summary);
                b.Property(p => p.TypeName).IsRequired().HasMaxLength(OneCodeConsts.DbLength_20);
                b.Property(p => p.SourceName).IsRequired().HasMaxLength(OneCodeConsts.DbLength_20);
                b.Property(p => p.Url).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.KvUrl).HasMaxLength(OneCodeConsts.DbLength_Url);
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
                b.Property(p => p.CommisionRate).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                b.Property(p => p.CommisionValue).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                b.Property(p => p.CityStart).HasMaxLength(OneCodeConsts.DbLength_City);
                b.Property(p => p.CityEnd).HasMaxLength(OneCodeConsts.DbLength_City);

                //外部编号(产品系统内编号)--新建产品后必须绑定外部编号才允许关联店铺
                b.Property(p => p.OutterId).HasMaxLength(OneCodeConsts.DbLength_Name);

                b.ConfigureByConvention();

            });

            builder.Entity<ShopProduct>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "ShopProducts", OneCodeConsts.DbSchema);
                b.Property(p => p.CommisionRate).HasColumnType("decimal(18,2)");
                b.Property(p => p.CommisionValue).HasColumnType("decimal(18,2)");

                //设置复合主键
                b.HasKey(sp => new { sp.ProductId, sp.ShopId });

                b.HasOne(p => p.Shop).WithMany(p => p.ShopProducts).HasForeignKey(p => p.ShopId);
                b.HasOne(p => p.Product).WithMany(p => p.ShopProducts).HasForeignKey(p => p.ProductId);

            });

            builder.Entity<Order>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Orders", OneCodeConsts.DbSchema);

                b.Property(p => p.Title).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Title);
                b.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
                b.Property(p => p.TotalCommision).HasColumnType("decimal(18,2)");
                b.Property(p => p.TotalPorfit).HasColumnType("decimal(18,2)");
                b.Property(p => p.SettlementAmount).HasColumnType("decimal(18,2)");
                b.Property(p => p.OutterOrderId).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Name);

                b.ConfigureByConvention();

                b.HasMany(p => p.OrderDetails).WithOne().HasForeignKey(p => p.OrderId);
            });

            builder.Entity<OrderDetail>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "OrderDetails", OneCodeConsts.DbSchema);

                b.Property(p => p.OutterProductId).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Name);
                b.Property(p => p.ProductTitle).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Title);
                b.Property(p => p.ProductTypeName).IsRequired().HasMaxLength(OneCodeConsts.DbLength_20);

                b.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                b.Property(p => p.Profit).HasColumnType("decimal(18,2)");
                b.Property(p => p.CommisionRate).HasColumnType("decimal(18,2)");
                b.Property(p => p.CommisionValue).HasColumnType("decimal(18,2)");
                b.Property(p => p.Commision).HasColumnType("decimal(18,2)");
                b.Property(p => p.RefundBalance).HasColumnType("decimal(18,2)");
                b.Property(p => p.RemainAmount).HasColumnType("decimal(18,2)");
                b.Property(p => p.RemainSettlementAmount).HasColumnType("decimal(18,2)");

                b.ConfigureByConvention();
            });

            builder.Entity<Draw>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "Draws", OneCodeConsts.DbSchema);

                b.Property(p => p.Name).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Name);
                b.Property(p => p.Mobile).IsRequired().HasMaxLength(OneCodeConsts.DbLength_Mobile);
                b.Property(p => p.ShopName).IsRequired().HasMaxLength(OneCodeConsts.DbLength_ShopName);
                b.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                b.Property(p => p.RemainCommision).HasColumnType("decimal(18,2)");

                b.ConfigureByConvention();
            });

            builder.Entity<CommisionRecord>(b =>
            {
                b.ToTable(OneCodeConsts.DbTablePrefix + "CommisionRecords", OneCodeConsts.DbSchema);

                b.Property(p => p.SalerName).HasMaxLength(OneCodeConsts.DbLength_Name);
                b.Property(p => p.ShopName).HasMaxLength(OneCodeConsts.DbLength_ShopName);
                b.Property(p => p.CommisionAmount).HasColumnType("decimal(18,2)");
                b.Property(p => p.CommisionAvailable).HasColumnType("decimal(18,2)");

                b.ConfigureByConvention();
            });
        }
    }
}