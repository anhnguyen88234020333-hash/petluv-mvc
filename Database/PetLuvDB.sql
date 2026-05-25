-- 1. Tạo mới Database
CREATE DATABASE PetLuvDB;
GO
-- 2. Bắt buộc sử dụng đúng PetLuvDB
USE PetLuvDB;
GO
DROP TABLE IF EXISTS Cart;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    Password NVARCHAR(100),
    Role NVARCHAR(20)
);
-- 3. Tạo bảng Sản phẩm
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100),
    Price DECIMAL(18,2),
    Stock INT,
    Description NVARCHAR(MAX),
    ImageURL NVARCHAR(255),
    Category NVARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Cart (
    CartID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
INSERT INTO Products (ProductName, Price, Stock, Description, ImageURL, Category)
VALUES 
(N'SmartHeart Adult Dog Food', 100000, 50, N'Dinh dưỡng hoàn chỉnh cho chó trưởng thành.', '~/images/products/1-hat-cho-adult-smartheart.jpg', N'Thức ăn chó'),
(N'Hạt hữu cơ ANF vị cừu', 120000, 30, N'Thành phần hữu cơ, tốt cho da và tiêu hóa.', '~/images/products/2-hat-cho-anf-vi-cuu.jpg', N'Thức ăn chó'),
(N'Royal Canin Mini Puppy', 185000, 25, N'Dành cho chó con cỡ nhỏ dưới 10 tháng tuổi.', '~/images/products/3-hat-cho-con-royal-canin.jpg', N'Thức ăn chó'),
(N'Today Dinner Puppy', 350000, 15, N'Hạt dinh dưỡng cao cấp vị gà thơm ngon.', '~/images/products/4-hat-cho-con-today-dinner.jpg', N'Thức ăn chó'),
(N'LuvCare cho chó trưởng thành', 210000, 20, N'Giúp lông bóng mượt và giảm mùi hôi chất thải.', '~/images/products/5-hat-cho-luv-care-truong-thanh.jpg', N'Thức ăn chó'),
(N'Nutrience vị cừu cho chó', 280000, 15, N'Công thức từ Canada, giàu đạm động vật.', '~/images/products/6-hat-cho-nutrience-vi-cuu.jpg', N'Thức ăn chó'),
(N'Reflex vị cá hồi cho chó', 190000, 20, N'Bổ sung Omega 3 & 6 cho bộ lông khỏe mạnh.', '~/images/products/7-hat-cho-reflex-vi-ca-hoi.jpg', N'Thức ăn chó'),
(N'Pedigree cho chó trưởng thành', 130000, 40, N'Hạt vị bò và rau củ, cung cấp đủ năng lượng.', '~/images/products/8-hat-cho-truong-thanh-pedigree.jpg', N'Thức ăn chó'),
(N'Hạt Cat Eye All Stages', 160000, 35, N'Hạt cho mèo mọi lứa tuổi, giúp kiểm soát búi lông.', '~/images/products/9-hat-meo-cat-eye.jpg', N'Thức ăn mèo'),
(N'Whiskas cho mèo con', 30000, 100, N'Pate và hạt cung cấp Canxi cho mèo phát triển.', '~/images/products/10-hat-meo-con-whiskas.jpg', N'Thức ăn mèo'),
(N'Thức ăn mèo Me-O', 95000, 50, N'Vị cá thu thơm ngon, kích thích vị giác của mèo.', '~/images/products/11-hat-meo-meo.jpg', N'Thức ăn mèo'),
(N'Hạt Minino cho mèo', 85000, 45, N'Dinh dưỡng cân bằng theo tiêu chuẩn Pháp.', '~/images/products/12-hat-meo-minino.jpg', N'Thức ăn mèo'),
(N'Hạt Mr. Vet cho mèo', 110000, 25, N'Hạt cao cấp giúp tăng cường hệ miễn dịch.', '~/images/products/13-hat-meo-mr-vet.jpg', N'Thức ăn mèo'),
(N'Nutrience cho mèo', 250000, 15, N'Nguồn nguyên liệu tự nhiên, không ngũ cốc.', '~/images/products/14-hat-meo-nutrience.jpg', N'Thức ăn mèo'),
(N'Hạt Reflex cho mèo', 180000, 30, N'Cân bằng dinh dưỡng, hỗ trợ hệ tiết niệu.', '~/images/products/15-hat-meo-reflex.jpg', N'Thức ăn mèo'),
(N'Royal Canin cho mèo', 230000, 20, N'Sản phẩm chuyên biệt cho các dòng mèo khác nhau.', '~/images/products/16-hat-meo-royal-canin.jpg', N'Thức ăn mèo');

--fix trang chủ
USE PetLuvDB;
GO

-- Bước 1: Xóa toàn bộ dữ liệu cũ để làm mới lại từ đầu
DELETE FROM Products;

-- Bước 2: Reset con số ID quay về số 0 để khi nạp món mới nó sẽ bắt đầu từ 1
DBCC CHECKIDENT ('Products', RESEED, 0);

-- Bước 3: Nạp lại 4 sản phẩm theo đúng thứ tự Ngọc vừa dò
INSERT INTO Products (ProductName, Price, Stock, Description, ImageURL, Category)
VALUES 
(N'SmartHeart Adult Dog Food', 100000, 30, N'Dinh dưỡng hoàn chỉnh cho chó trưởng thành.', '~/images/products/1-hat-cho-adult-smartheart.jpg', N'Thức ăn'), -- Sẽ có ID = 1
(N'Hạt hữu cơ ANF 6 vị cừu', 99000, 40, N'Sản phẩm hữu cơ tốt cho tiêu hóa của cún.', '~/images/products/2-hat-cho-anf-vi-cuu.jpg', N'Thức ăn'), -- Sẽ có ID = 2
(N'Royal Canin Mini Puppy', 185000, 25, N'Hạt dành riêng cho chó con cỡ nhỏ.', '~/images/products/3-hat-cho-con-royal-canin.jpg', N'Thức ăn'), -- Sẽ có ID = 3
(N'Today Dinner Puppy', 350000, 10, N'Thức ăn cao cấp vị gà thơm ngon.', '~/images/products/4-hat-cho-con-today-dinner.jpg', N'Thức ăn'); -- Sẽ có ID = 4

--fix giỏ hàng
USE PetLuvDB;
GO

ALTER TABLE Cart
ADD DateAdded DATETIME DEFAULT GETDATE();

USE PetLuvDB;
GO

-- Kiểm tra và thêm người dùng số 1 nếu chưa có
IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = 1)
BEGIN
    SET IDENTITY_INSERT Users ON; -- Cho phép tự điền ID là 1
    INSERT INTO Users (UserID, FullName, Email, Password, Role) 
    VALUES (1, N'Khổng Bảo Ngọc', 'ngoc@ueh.edu.vn', '123', 'Customer');
    SET IDENTITY_INSERT Users OFF;
END

--fix data của chi tiết đơn hàng
USE PetLuvDB;
GO
-- Thêm các cột còn thiếu để khớp hoàn toàn với code C# của nhóm
ALTER TABLE dbo.Orders ADD CustomerName NVARCHAR(255) NULL;
ALTER TABLE dbo.Orders ADD Address NVARCHAR(MAX) NULL;
ALTER TABLE dbo.Orders ADD Phone VARCHAR(50) NULL;
ALTER TABLE dbo.Orders ADD Status INT NOT NULL DEFAULT 0;
GO

USE PetLuvDB;
GO

USE PetLuvDB;
GO

-- Nạp tài khoản Admin
INSERT INTO dbo.Users (FullName, Email, Password, Role)
VALUES (N'Admin PetLuv', 'petluv@gmail.com', '123456', 'Admin');
GO

--Thêm tài khoản
USE PetLuvDB;
GO
UPDATE dbo.Users SET Role = 'Admin' WHERE Email = '1304.khongngoc@gmail.com';
GO

--Thêm sản phẩm phụ kiện
INSERT INTO Products (ProductName, Price, Stock, Description, ImageURL, Category)
VALUES 
(N'Vòng cổ quả chuông đệm da mềm', 45000, 50, N'Thiết kế chuông bạc nhỏ xinh kèm lớp đệm da êm ái cho boss.', '17-vong-co-chuong.jpg', N'Phụ kiện'),
(N'Bát ăn gốm sứ chống kiến cao cấp', 120000, 30, N'Chất liệu gốm dày dặn, dễ vệ sinh, thiết kế máng chống kiến bò.', '18-bat-an-gom-su.jpg', N'Phụ kiện'),
(N'Nhà cây Cat Tree gỗ hiện đại', 850000, 10, N'Nhà cây mini bằng gỗ tự nhiên, tích hợp cột cào móng siêu bền.', '19-nha-cay-cat-tree.jpg', N'Phụ kiện'),
(N'Áo nỉ Hoodie sọc Bear dễ thương', 75000, 40, N'Vải nỉ cotton co giãn 4 chiều, giữ ấm tốt và cực kỳ thời trang.', '20-ao-hoodie-thucung.jpg', N'Phụ kiện'),
(N'Đệm nằm bông vương miện hoàng gia', 250000, 15, N'Lớp bông PP siêu êm, bọc vải nhung mềm mại cho giấc ngủ hoàng gia.', '21-dem-nam-hoang-gia.jpg', N'Phụ kiện'),
(N'Đồ chơi cần câu lông vũ tương tác', 25000, 100, N'Giúp kích thích vận động và xả stress hiệu quả cho các bé mèo.', '22-can-cau-long-vu.jpg', N'Phụ kiện');
GO

