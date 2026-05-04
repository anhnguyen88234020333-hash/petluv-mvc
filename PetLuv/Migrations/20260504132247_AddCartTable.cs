using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetLuv.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 20);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "Description",
                value: "Hạt dinh dưỡng cân bằng protein và chất béo giúp chó trưởng thành duy trì vóc dáng và sức khỏe tốt.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "Description",
                value: "Sản phẩm hữu cơ với thành phần thịt cừu tươi ngon, hỗ trợ hệ tiêu hóa và làm mượt lông cho cún yêu.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "Description",
                value: "Công thức đặc biệt dành cho chó con giống nhỏ, bổ sung DHA giúp phát triển trí não và tăng cường miễn dịch.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "Description",
                value: "Thức ăn hạt vị cá hồi giàu Omega-3, không gây dị ứng, phù hợp cho sự phát triển toàn diện của chó con.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "Description",
                value: "Dinh dưỡng chuyên biệt cho chó giống nhỏ, giúp giảm mảng bám trên răng và ổn định hệ đường ruột.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "Description",
                value: "Thành phần tự nhiên từ thịt cừu và gạo lứt, không chứa chất bảo quản, cực kỳ an toàn cho sức khỏe thú cưng.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "Description",
                value: "Thức ăn siêu cao cấp với cá hồi Nauy, giúp cải thiện tình trạng da liễu và kích thích vị giác chó kén ăn.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "Description",
                value: "Sự kết hợp hoàn hảo giữa thịt gà và rau củ, cung cấp đầy đủ vitamin và khoáng chất cho hoạt động hàng ngày.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "Description",
                value: "Hạt dinh dưỡng tổng hợp cho mọi lứa tuổi, hỗ trợ đào thải búi lông và giảm mùi hôi chất thải của mèo.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10,
                column: "Description",
                value: "Thức ăn giàu Canxi và Phospho giúp mèo con phát triển xương chắc khỏe trong giai đoạn từ 2 đến 12 tháng.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11,
                column: "Description",
                value: "Vị hải sản thơm ngon khó cưỡng, bổ sung Taurine giúp mắt mèo sáng tinh anh và bảo vệ hệ tim mạch.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12,
                column: "Description",
                value: "Hạt vị mực hấp dẫn, kết hợp các sợi xơ tự nhiên giúp hệ tiêu hóa của mèo hoạt động trơn tru hơn.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13,
                column: "Description",
                value: "Công thức Grain-Free không ngũ cốc, giàu đạm từ cá ngừ tươi, dành riêng cho các bé mèo có hệ tiêu hóa nhạy cảm.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14,
                column: "Description",
                value: "Thức ăn sấy lạnh cao cấp từ thịt bò và cá, cung cấp năng lượng vượt trội cho mèo năng động.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15,
                column: "Description",
                value: "Dinh dưỡng dễ hấp thụ từ thịt gà sạch, giúp mèo con lớn nhanh và có hệ miễn dịch vượt trội.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 16,
                column: "Description",
                value: "Sản phẩm đặc chế cho mèo mẹ đang mang thai và mèo con mới tập ăn, kết cấu hạt mềm dễ nhai.");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "Description",
                value: "Hạt dinh dưỡng dành cho chó trưởng thành, cung cấp đầy đủ vitamin...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "Description",
                value: "Hạt cao cấp vị cừu dành cho chó, giàu protein và dưỡng chất...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "Description",
                value: "Hạt dành riêng cho chó con giống nhỏ từ 2 đến 10 tháng tuổi...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "Description",
                value: "Thức ăn hạt vị cá hồi dành cho chó con dưới 12 tháng tuổi...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "Description",
                value: "Hạt dinh dưỡng cho chó trưởng thành giống nhỏ, nuôi dưỡng da lông...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "Description",
                value: "Thành phần chính là thịt cừu và gạo lứt, lý tưởng cho tiêu hóa...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "Description",
                value: "Thức ăn siêu cao cấp vị cá hồi dành cho chó trưởng thành...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "Description",
                value: "Hạt dinh dưỡng vị gà và rau củ cho chó trưởng thành trên 1 năm tuổi...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "Description",
                value: "Thức ăn cho mọi lứa tuổi, giúp kiểm soát búi lông và tăng thị lực...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10,
                column: "Description",
                value: "Dành cho mèo con từ 2–12 tháng tuổi, hỗ trợ phát triển toàn diện...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11,
                column: "Description",
                value: "Hương vị hải sản thơm ngon, tăng cường thị lực cho mèo...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12,
                column: "Description",
                value: "Vị mực hấp dẫn, bổ sung taurine tốt cho mắt và tim mạch...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13,
                column: "Description",
                value: "Hạt không chứa ngũ cốc, phù hợp cho mèo nhạy cảm tiêu hóa...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14,
                column: "Description",
                value: "Giàu protein từ thịt tươi bò, heo rừng và cá sấy lạnh...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15,
                column: "Description",
                value: "Dành riêng cho mèo con, dễ tiêu hóa và tăng cường miễn dịch...");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 16,
                column: "Description",
                value: "Dành cho mèo mẹ mang thai và mèo con từ 1–4 tháng tuổi...");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "ImageURL", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 17, "Dịch vụ", "Không gian sạch sẽ, chăm sóc 24/7 giúp thú cưng thoải mái...", "service-hotel-1.jpg", 200000m, "Pet Hotel - Lưu trú", 0 },
                    { 18, "Dịch vụ", "Môi trường an toàn, đội ngũ chăm sóc tận tâm chu đáo...", "service-hotel-2.jpg", 150000m, "Dịch vụ giữ thú cưng", 0 },
                    { 19, "Dịch vụ", "Tắm, cắt tỉa và chăm sóc lông chuyên nghiệp giúp thú cưng sạch thơm...", "service-spa-1.jpg", 300000m, "Pet Spa & Grooming", 0 },
                    { 20, "Dịch vụ", "Quy trình chăm sóc nhẹ nhàng, giúp thú cưng thư giãn nhất...", "service-spa-2.jpg", 250000m, "Chăm sóc & làm đẹp", 0 }
                });
        }
    }
}
