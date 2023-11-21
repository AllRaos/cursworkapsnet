using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursWork.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourierInfo",
                columns: table => new
                {
                    CourierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryPay = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourierInfo", x => x.CourierId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    House = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true),
                    AcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryList",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId1 = table.Column<int>(type: "int", nullable: false),
                    CourierId1 = table.Column<int>(type: "int", nullable: false),
                    ReceivingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryList", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_DeliveryList_CourierInfo_CourierId1",
                        column: x => x.CourierId1,
                        principalTable: "CourierInfo",
                        principalColumn: "CourierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryList_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    OrdersProductsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId1 = table.Column<int>(type: "int", nullable: true),
                    ProductId1 = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => x.OrdersProductsId);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "CheckList",
                columns: table => new
                {
                    CheckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId1 = table.Column<int>(type: "int", nullable: true),
                    FirstNameCustomerId = table.Column<int>(type: "int", nullable: true),
                    LastNameCustomerId = table.Column<int>(type: "int", nullable: true),
                    TotalDeliveryId = table.Column<int>(type: "int", nullable: true),
                    AcceptenceDateOrderId = table.Column<int>(type: "int", nullable: true),
                    ReceivingDateDeliveryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckList", x => x.CheckId);
                    table.ForeignKey(
                        name: "FK_CheckList_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_CheckList_Customers_FirstNameCustomerId",
                        column: x => x.FirstNameCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_CheckList_Customers_LastNameCustomerId",
                        column: x => x.LastNameCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_CheckList_DeliveryList_ReceivingDateDeliveryId",
                        column: x => x.ReceivingDateDeliveryId,
                        principalTable: "DeliveryList",
                        principalColumn: "DeliveryId");
                    table.ForeignKey(
                        name: "FK_CheckList_DeliveryList_TotalDeliveryId",
                        column: x => x.TotalDeliveryId,
                        principalTable: "DeliveryList",
                        principalColumn: "DeliveryId");
                    table.ForeignKey(
                        name: "FK_CheckList_Orders_AcceptenceDateOrderId",
                        column: x => x.AcceptenceDateOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_AcceptenceDateOrderId",
                table: "CheckList",
                column: "AcceptenceDateOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_CustomerId1",
                table: "CheckList",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_FirstNameCustomerId",
                table: "CheckList",
                column: "FirstNameCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_LastNameCustomerId",
                table: "CheckList",
                column: "LastNameCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_ReceivingDateDeliveryId",
                table: "CheckList",
                column: "ReceivingDateDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_TotalDeliveryId",
                table: "CheckList",
                column: "TotalDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryList_CourierId1",
                table: "DeliveryList",
                column: "CourierId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryList_OrderId1",
                table: "DeliveryList",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_OrderId1",
                table: "OrdersProducts",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ProductId1",
                table: "OrdersProducts",
                column: "ProductId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckList");

            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "DeliveryList");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CourierInfo");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
