using Microsoft.EntityFrameworkCore;
using PetLuv.Models;

namespace PetLuv.Data
{
    public class PetLuvDbContext : DbContext
    {
        public PetLuvDbContext(DbContextOptions<PetLuvDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "SmartHeart Adult Dog Food", Price = 100000, ImageURL = "1-hat-cho-adult-smartheart.jpg", Description = "Hạt dinh dưỡng cho chó trưởng thành...", Category = "Thức ăn chó" },
                new Product { ProductID = 2, ProductName = "Hạt hữu cơ ANF 6 vị cừu", Price = 99000, ImageURL = "2-hat-cho-anf-vi-cuu.jpg", Description = "Hạt cao cấp vị cừu dành cho chó...", Category = "Thức ăn chó" },
                new Product { ProductID = 3, ProductName = "Royal Canin Mini Puppy", Price = 185000, ImageURL = "3-hat-cho-con-royal-canin.jpg", Description = "Hạt dành riêng cho chó con giống nhỏ...", Category = "Thức ăn chó" },
                new Product { ProductID = 4, ProductName = "Today's Dinner Puppy Salmon", Price = 350000, ImageURL = "4-hat-cho-con-today-dinner.jpg", Description = "Thức ăn hạt vị cá hồi dành cho chó con...", Category = "Thức ăn chó" },
                new Product { ProductID = 5, ProductName = "LuvCare Adult Small Breed", Price = 310000, ImageURL = "5-hat-cho-luv-care-truong-thanh.jpg", Description = "Hạt dinh dưỡng cho chó trưởng thành giống nhỏ...", Category = "Thức ăn chó" },
                new Product { ProductID = 6, ProductName = "Nutrience Original Adult", Price = 520000, ImageURL = "6-hat-cho-nutrience-vi-cuu.jpg", Description = "Thành phần chính là thịt cừu và gạo lứt...", Category = "Thức ăn chó" },
                new Product { ProductID = 7, ProductName = "Reflex Plus Adult Salmon", Price = 320000, ImageURL = "7-hat-cho-reflex-vi-ca-hoi.jpg", Description = "Thức ăn siêu cao cấp vị cá hồi...", Category = "Thức ăn chó" },
                new Product { ProductID = 8, ProductName = "Pedigree Adult Chicken", Price = 110000, ImageURL = "8-hat-cho-truong-thanh-pedigree.jpg", Description = "Hạt vị gà và rau củ cho chó trưởng thành...", Category = "Thức ăn chó" },
                new Product { ProductID = 9, ProductName = "Cat's Eye Kitten & Cat", Price = 820000, ImageURL = "9-hat-meo-cat-eye.jpg", Description = "Thức ăn mọi lứa tuổi, kiểm soát búi lông...", Category = "Thức ăn mèo" },
                new Product { ProductID = 10, ProductName = "Whiskas Junior Cat Food", Price = 120000, ImageURL = "10-hat-meo-con-whiskas.jpg", Description = "Dành cho mèo con 2–12 tháng tuổi...", Category = "Thức ăn mèo" },
                new Product { ProductID = 11, ProductName = "Me-O Adult Cat Food", Price = 95000, ImageURL = "11-hat-meo-meo.jpg", Description = "Hương vị hải sản thơm ngon...", Category = "Thức ăn mèo" },
                new Product { ProductID = 12, ProductName = "Minino Yum Adult Cat", Price = 90000, ImageURL = "12-hat-meo-minino.jpg", Description = "Vị mực hấp dẫn, tốt cho mắt...", Category = "Thức ăn mèo" },
                new Product { ProductID = 13, ProductName = "Mr.Vet Grain Free Tuna", Price = 130000, ImageURL = "13-hat-meo-mr-vet.jpg", Description = "Hạt không ngũ cốc cho mèo nhạy cảm...", Category = "Thức ăn mèo" },
                new Product { ProductID = 14, ProductName = "Nutrience SubZero Red", Price = 280000, ImageURL = "14-hat-meo-nutrience.jpg", Description = "Giàu protein từ thịt tươi bò, cá sấy lạnh...", Category = "Thức ăn mèo" },
                new Product { ProductID = 15, ProductName = "Reflex Plus Kitten Chicken", Price = 95000, ImageURL = "15-hat-meo-reflex.jpg", Description = "Thịt gà dễ tiêu hóa cho mèo con...", Category = "Thức ăn mèo" },
                new Product { ProductID = 16, ProductName = "Royal Canin Mother & Babycat", Price = 220000, ImageURL = "16-hat-meo-royal-canin.jpg", Description = "Dành cho mèo mẹ và mèo con 1–4 tháng...", Category = "Thức ăn mèo" }
            ); 
        }
    }
}