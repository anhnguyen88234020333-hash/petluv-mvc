using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetLuv.Migrations
{
    /// <inheritdoc />
    public partial class SeedFinalDataV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Category", "Description", "ImageURL", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 1, "Thức ăn chó", "Hạt dinh dưỡng dành cho chó trưởng thành, cung cấp đầy đủ vitamin...", "1-hat-cho-adult-smartheart.jpg", 100000m, "SmartHeart Adult Dog Food", 0 },
                    { 2, "Thức ăn chó", "Hạt cao cấp vị cừu dành cho chó, giàu protein và dưỡng chất...", "2-hat-cho-anf-vi-cuu.jpg", 99000m, "Hạt hữu cơ ANF 6 vị cừu", 0 },
                    { 3, "Thức ăn chó", "Hạt dành riêng cho chó con giống nhỏ từ 2 đến 10 tháng tuổi...", "3-hat-cho-con-royal-canin.jpg", 185000m, "Royal Canin Mini Puppy", 0 },
                    { 4, "Thức ăn chó", "Thức ăn hạt vị cá hồi dành cho chó con dưới 12 tháng tuổi...", "4-hat-cho-con-today-dinner.jpg", 350000m, "Today's Dinner Puppy Salmon", 0 },
                    { 5, "Thức ăn chó", "Hạt dinh dưỡng cho chó trưởng thành giống nhỏ, nuôi dưỡng da lông...", "5-hat-cho-luv-care-truong-thanh.jpg", 310000m, "LuvCare Adult Small Breed", 0 },
                    { 6, "Thức ăn chó", "Thành phần chính là thịt cừu và gạo lứt, lý tưởng cho tiêu hóa...", "6-hat-cho-nutrience-vi-cuu.jpg", 520000m, "Nutrience Original Adult", 0 },
                    { 7, "Thức ăn chó", "Thức ăn siêu cao cấp vị cá hồi dành cho chó trưởng thành...", "7-hat-cho-reflex-vi-ca-hoi.jpg", 320000m, "Reflex Plus Adult Salmon", 0 },
                    { 8, "Thức ăn chó", "Hạt dinh dưỡng vị gà và rau củ cho chó trưởng thành trên 1 năm tuổi...", "8-hat-cho-truong-thanh-pedigree.jpg", 110000m, "Pedigree Adult Chicken", 0 },
                    { 9, "Thức ăn mèo", "Thức ăn cho mọi lứa tuổi, giúp kiểm soát búi lông và tăng thị lực...", "9-hat-meo-cat-eye.jpg", 820000m, "Cat's Eye Kitten & Cat", 0 },
                    { 10, "Thức ăn mèo", "Dành cho mèo con từ 2–12 tháng tuổi, hỗ trợ phát triển toàn diện...", "10-hat-meo-con-whiskas.jpg", 120000m, "Whiskas Junior Cat Food", 0 },
                    { 11, "Thức ăn mèo", "Hương vị hải sản thơm ngon, tăng cường thị lực cho mèo...", "11-hat-meo-meo.jpg", 95000m, "Me-O Adult Cat Food", 0 },
                    { 12, "Thức ăn mèo", "Vị mực hấp dẫn, bổ sung taurine tốt cho mắt và tim mạch...", "12-hat-meo-minino.jpg", 90000m, "Minino Yum Adult Cat", 0 },
                    { 13, "Thức ăn mèo", "Hạt không chứa ngũ cốc, phù hợp cho mèo nhạy cảm tiêu hóa...", "13-hat-meo-mr-vet.jpg", 130000m, "Mr.Vet Grain Free Tuna", 0 },
                    { 14, "Thức ăn mèo", "Giàu protein từ thịt tươi bò, heo rừng và cá sấy lạnh...", "14-hat-meo-nutrience.jpg", 280000m, "Nutrience SubZero Red", 0 },
                    { 15, "Thức ăn mèo", "Dành riêng cho mèo con, dễ tiêu hóa và tăng cường miễn dịch...", "15-hat-meo-reflex.jpg", 95000m, "Reflex Plus Kitten Chicken", 0 },
                    { 16, "Thức ăn mèo", "Dành cho mèo mẹ mang thai và mèo con từ 1–4 tháng tuổi...", "16-hat-meo-royal-canin.jpg", 220000m, "Royal Canin Mother & Babycat", 0 },
                    { 17, "Dịch vụ", "Không gian sạch sẽ, chăm sóc 24/7 giúp thú cưng thoải mái...", "service-hotel-1.jpg", 200000m, "Pet Hotel - Lưu trú", 0 },
                    { 18, "Dịch vụ", "Môi trường an toàn, đội ngũ chăm sóc tận tâm chu đáo...", "service-hotel-2.jpg", 150000m, "Dịch vụ giữ thú cưng", 0 },
                    { 19, "Dịch vụ", "Tắm, cắt tỉa và chăm sóc lông chuyên nghiệp giúp thú cưng sạch thơm...", "service-spa-1.jpg", 300000m, "Pet Spa & Grooming", 0 },
                    { 20, "Dịch vụ", "Quy trình chăm sóc nhẹ nhàng, giúp thú cưng thư giãn nhất...", "service-spa-2.jpg", 250000m, "Chăm sóc & làm đẹp", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 16);

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
        }
    }
}
