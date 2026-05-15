using Microsoft.EntityFrameworkCore;
using PetLuv.Models;

namespace PetLuv.Data
{
    public class PetLuvDbContext : DbContext
    {
        public PetLuvDbContext(DbContextOptions<PetLuvDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        
        // Phụng nhớ phải có dòng này thì trang Đăng nhập mới chạy được nè!
        public DbSet<User> Users { get; set; } 
        // Thêm dòng này để Entity Framework hiểu và tạo bảng Cart
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu 16 món đã chỉnh sửa mô tả đầy đủ
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "SmartHeart Adult Dog Food", Price = 100000, ImageURL = "1-hat-cho-adult-smartheart.jpg", Description = "Hạt dinh dưỡng cân bằng protein và chất béo giúp chó trưởng thành duy trì vóc dáng và sức khỏe tốt.", Category = "Thức ăn chó" },
                new Product { ProductID = 2, ProductName = "Hạt hữu cơ ANF 6 vị cừu", Price = 99000, ImageURL = "2-hat-cho-anf-vi-cuu.jpg", Description = "Sản phẩm hữu cơ với thành phần thịt cừu tươi ngon, hỗ trợ hệ tiêu hóa và làm mượt lông cho cún yêu.", Category = "Thức ăn chó" },
                new Product { ProductID = 3, ProductName = "Royal Canin Mini Puppy", Price = 185000, ImageURL = "3-hat-cho-con-royal-canin.jpg", Description = "Công thức đặc biệt dành cho chó con giống nhỏ, bổ sung DHA giúp phát triển trí não và tăng cường miễn dịch.", Category = "Thức ăn chó" },
                new Product { ProductID = 4, ProductName = "Today's Dinner Puppy Salmon", Price = 350000, ImageURL = "4-hat-cho-con-today-dinner.jpg", Description = "Thức ăn hạt vị cá hồi giàu Omega-3, không gây dị ứng, phù hợp cho sự phát triển toàn diện của chó con.", Category = "Thức ăn chó" },
                new Product { ProductID = 5, ProductName = "LuvCare Adult Small Breed", Price = 310000, ImageURL = "5-hat-cho-luv-care-truong-thanh.jpg", Description = "Dinh dưỡng chuyên biệt cho chó giống nhỏ, giúp giảm mảng bám trên răng và ổn định hệ đường ruột.", Category = "Thức ăn chó" },
                new Product { ProductID = 6, ProductName = "Nutrience Original Adult", Price = 520000, ImageURL = "6-hat-cho-nutrience-vi-cuu.jpg", Description = "Thành phần tự nhiên từ thịt cừu và gạo lứt, không chứa chất bảo quản, cực kỳ an toàn cho sức khỏe thú cưng.", Category = "Thức ăn chó" },
                new Product { ProductID = 7, ProductName = "Reflex Plus Adult Salmon", Price = 320000, ImageURL = "7-hat-cho-reflex-vi-ca-hoi.jpg", Description = "Thức ăn siêu cao cấp với cá hồi Nauy, giúp cải thiện tình trạng da liễu và kích thích vị giác chó kén ăn.", Category = "Thức ăn chó" },
                new Product { ProductID = 8, ProductName = "Pedigree Adult Chicken", Price = 110000, ImageURL = "8-hat-cho-truong-thanh-pedigree.jpg", Description = "Sự kết hợp hoàn hảo giữa thịt gà và rau củ, cung cấp đầy đủ vitamin và khoáng chất cho hoạt động hàng ngày.", Category = "Thức ăn chó" },
                new Product { ProductID = 9, ProductName = "Cat's Eye Kitten & Cat", Price = 820000, ImageURL = "9-hat-meo-cat-eye.jpg", Description = "Hạt dinh dưỡng tổng hợp cho mọi lứa tuổi, hỗ trợ đào thải búi lông và giảm mùi hôi chất thải của mèo.", Category = "Thức ăn mèo" },
                new Product { ProductID = 10, ProductName = "Whiskas Junior Cat Food", Price = 120000, ImageURL = "10-hat-meo-con-whiskas.jpg", Description = "Thức ăn giàu Canxi và Phospho giúp mèo con phát triển xương chắc khỏe trong giai đoạn từ 2 đến 12 tháng.", Category = "Thức ăn mèo" },
                new Product { ProductID = 11, ProductName = "Me-O Adult Cat Food", Price = 95000, ImageURL = "11-hat-meo-meo.jpg", Description = "Vị hải sản thơm ngon khó cưỡng, bổ sung Taurine giúp mắt mèo sáng tinh anh và bảo vệ hệ tim mạch.", Category = "Thức ăn mèo" },
                new Product { ProductID = 12, ProductName = "Minino Yum Adult Cat", Price = 90000, ImageURL = "12-hat-meo-minino.jpg", Description = "Hạt vị mực hấp dẫn, kết hợp các sợi xơ tự nhiên giúp hệ tiêu hóa của mèo hoạt động trơn tru hơn.", Category = "Thức ăn mèo" },
                new Product { ProductID = 13, ProductName = "Mr.Vet Grain Free Tuna", Price = 130000, ImageURL = "13-hat-meo-mr-vet.jpg", Description = "Công thức Grain-Free không ngũ cốc, giàu đạm từ cá ngừ tươi, dành riêng cho các bé mèo có hệ tiêu hóa nhạy cảm.", Category = "Thức ăn mèo" },
                new Product { ProductID = 14, ProductName = "Nutrience SubZero Red", Price = 280000, ImageURL = "14-hat-meo-nutrience.jpg", Description = "Thức ăn sấy lạnh cao cấp từ thịt bò và cá, cung cấp năng lượng vượt trội cho mèo năng động.", Category = "Thức ăn mèo" },
                new Product { ProductID = 15, ProductName = "Reflex Plus Kitten Chicken", Price = 95000, ImageURL = "15-hat-meo-reflex.jpg", Description = "Dinh dưỡng dễ hấp thụ từ thịt gà sạch, giúp mèo con lớn nhanh và có hệ miễn dịch vượt trội.", Category = "Thức ăn mèo" },
                new Product { ProductID = 16, ProductName = "Royal Canin Mother & Babycat", Price = 220000, ImageURL = "16-hat-meo-royal-canin.jpg", Description = "Sản phẩm đặc chế cho mèo mẹ đang mang thai và mèo con mới tập ăn, kết cấu hạt mềm dễ nhai.", Category = "Thức ăn mèo" }
            );
        }
    }
}