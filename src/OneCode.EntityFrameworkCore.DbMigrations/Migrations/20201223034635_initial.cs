using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OcAdminUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    RealName = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcAdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcBizImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BizScope = table.Column<int>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false),
                    ThumbUrl = table.Column<string>(maxLength: 500, nullable: true),
                    OriginalUrl = table.Column<string>(maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcBizImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcDraws",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    ShopName = table.Column<string>(maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DrawStatus = table.Column<int>(nullable: false),
                    ConfirmTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcDraws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShopId = table.Column<Guid>(nullable: false),
                    SalerId = table.Column<Guid>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    FinishedDate = table.Column<DateTime>(nullable: false),
                    BizStatus = table.Column<int>(nullable: false),
                    OutterOrderId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Summary = table.Column<string>(maxLength: 300, nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 20, nullable: false),
                    SourceName = table.Column<string>(maxLength: 20, nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: false),
                    KvUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsOffShelf = table.Column<bool>(nullable: false),
                    IsSellOut = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    OutterId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcShops",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Summary = table.Column<string>(maxLength: 300, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Logo = table.Column<string>(maxLength: 500, nullable: true),
                    Kv = table.Column<string>(maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    Longitude = table.Column<string>(maxLength: 20, nullable: true),
                    Latitude = table.Column<string>(maxLength: 20, nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    OwnerName = table.Column<string>(maxLength: 50, nullable: true),
                    Template = table.Column<int>(nullable: false, defaultValue: 0),
                    IsShowOfficialLogo = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    CommissionRate = table.Column<decimal>(nullable: false),
                    CommissionAvailable = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CommissionApplying = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CommissionDoing = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OcOrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductTypeName = table.Column<string>(maxLength: 20, nullable: false),
                    ProductTitle = table.Column<string>(maxLength: 200, nullable: false),
                    OutterProductId = table.Column<string>(maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcOrderDetails_OcOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OcOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcSalers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Openid = table.Column<string>(maxLength: 50, nullable: true),
                    HeadImgUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Nickname = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ShopId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcSalers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcSalers_OcShops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "OcShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcShopProducts",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DispalyOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcShopProducts", x => new { x.ProductId, x.ShopId });
                    table.ForeignKey(
                        name: "FK_OcShopProducts_OcProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "OcProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcShopProducts_OcShops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "OcShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcShopTags",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcShopTags", x => new { x.ShopId, x.TagId });
                    table.ForeignKey(
                        name: "FK_OcShopTags_OcShops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "OcShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcShopTags_OcTags_TagId",
                        column: x => x.TagId,
                        principalTable: "OcTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OcOrderDetails_OrderId",
                table: "OcOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OcSalers_ShopId",
                table: "OcSalers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OcShopProducts_ShopId",
                table: "OcShopProducts",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_OcShopTags_TagId",
                table: "OcShopTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OcAdminUsers");

            migrationBuilder.DropTable(
                name: "OcBizImages");

            migrationBuilder.DropTable(
                name: "OcDraws");

            migrationBuilder.DropTable(
                name: "OcOrderDetails");

            migrationBuilder.DropTable(
                name: "OcSalers");

            migrationBuilder.DropTable(
                name: "OcShopProducts");

            migrationBuilder.DropTable(
                name: "OcShopTags");

            migrationBuilder.DropTable(
                name: "OcOrders");

            migrationBuilder.DropTable(
                name: "OcProducts");

            migrationBuilder.DropTable(
                name: "OcShops");

            migrationBuilder.DropTable(
                name: "OcTags");
        }
    }
}
