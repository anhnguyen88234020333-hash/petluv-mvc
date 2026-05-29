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

DELETE FROM Products;

DBCC CHECKIDENT ('Products', RESEED, 0);

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
    SET IDENTITY_INSERT Users ON; 
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

SELECT * FROM Products;

USE PetLuvDB;
GO

-- 1. Xóa sạch 6 món phụ kiện cũ 
DELETE FROM Products WHERE ProductID >= 17;

SET IDENTITY_INSERT Products ON;

INSERT INTO Products (ProductID, ProductName, Price, Stock, Description, ImageURL, Category)
VALUES 
(17, N'Vòng cổ quả chuông đệm da mềm', 45000, 50, N'Thiết kế chuông bạc nhỏ xinh kèm lớp đệm da êm ái cho boss.', '~/images/products/17-vong-co-chuong.jpg', N'Phụ kiện'),
(18, N'Bát ăn gốm sứ chống kiến cao cấp', 120000, 30, N'Chất liệu gốm dày dặn, dễ vệ sinh, thiết kế máng chống kiến bò.', '~/images/products/18-bat-an-gom-su.jpg', N'Phụ kiện'),
(19, N'Nhà cây Cat Tree gỗ hiện đại', 850000, 10, N'Nhà cây mini bằng gỗ tự nhiên, tích hợp cột cào móng siêu bền.', '~/images/products/19-nha-cay-cat-tree.jpg', N'Phụ kiện'),
(20, N'Áo nỉ Hoodie sọc Bear dễ thương', 75000, 40, N'Vải nỉ cotton co giãn 4 chiều, giữ ấm tốt và cực kỳ thời trang.', '~/images/products/20-ao-hoodie-thucung.jpg', N'Phụ kiện'),
(21, N'Đệm nằm bông vương miện hoàng gia', 250000, 15, N'Lớp bông PP siêu êm, bọc vải nhung mềm mại cho giấc ngủ hoàng gia.', '~/images/products/21-dem-nam-hoang-gia.jpg', N'Phụ kiện'),
(22, N'Đồ chơi cần câu lông vũ tương tác', 25000, 100, N'Giúp kích thích vận động và xả stress hiệu quả cho các bé mèo.', '~/images/products/22-can-cau-long-vu.jpg', N'Phụ kiện');

SET IDENTITY_INSERT Products OFF;
GO

--Thêm sản phẩm
INSERT INTO Products (ProductName, Price, Stock, Description, ImageURL, Category) VALUES

--THỨC ĂN
(N'Súp Thưởng Ciao Churu Vị Cá Hồi', 45000, 50, N'Súp thưởng thơm ngon kích thích vị giác tuyệt đối, bổ sung vitamin và giúp mượt lông cho mèo cưng.', '~/images/products/23-sup-thuong-ciao-ca-hoi.jpg', N'Thức ăn'),
(N'Hạt Cho Mèo Con Orijen Cat & Kitten', 290000, 20, N'Dòng hạt cao cấp chứa 85% thành phần thịt cá tươi nguyên chất, cung cấp nguồn đạm sinh học dồi dào.', '~/images/products/24-hat-meo-orijen-kitten.jpg', N'Thức ăn'),
(N'Pate Lon Morando Cho Chó Vị Thịt Gà', 65000, 35, N'Pate lon nhập khẩu từ Ý, kết cấu mềm mịn, giàu dinh dưỡng, thích hợp làm bữa ăn đổi vị cho cún.', '~/images/products/25-pate-lon-morando-vi-ga.jpg', N'Thức ăn'),
(N'Xương Gặm Canxi Sạch Răng Cho Cún', 35000, 40, N'Bánh thưởng dạng xương giúp cún giảm mảng bám, sạch răng thơm miệng và hạn chế cắn phá đồ đạc.', '~/images/products/26-banh-thuong-xuong-gam.jpg', N'Thức ăn'),
(N'Hạt Sỏi Thận Royal Canin Urinary S/O', 220000, 15, N'Thức ăn dinh dưỡng chuyên dụng cho mèo giúp hòa tan sỏi struvite và ngăn ngừa nguy cơ tái phát sỏi thận.', '~/images/products/27-hat-meo-royal-canin-urinary.jpg', N'Thức ăn'),

--PHỤ KIỆN
(N'Máy Lọc Nước Tự Động Đài Phun', 250000, 12, N'Hệ thống lọc nước tuần hoàn tự động giúp kích thích các Boss uống nhiều nước hơn, bảo vệ hệ tiết niệu.', '~/images/products/28-may-loc-nuoc-tu-dong.jpg', N'Phụ kiện'),
(N'Balo Phi Hành Gia Trong Suốt Cho Boss', 180000, 18, N'Thiết kế kính vòm trong suốt thời trang giúp Boss dễ dàng ngắm nhìn thế giới khi cùng Sen đi dạo phố.', '~/images/products/29-balo-phi-hanh-gia.jpg', N'Phụ kiện'),
(N'Xẻng Xúc Cát Vệ Sinh Nhựa PP Dày', 15000, 100, N'Chất liệu nhựa cao cấp siêu bền, thiết kế lỗ lọc chuẩn xác giúp dọn dẹp khay cát của Boss nhanh chóng.', '~/images/products/30-xeng-xuc-cat-ve-sinh.jpg', N'Phụ kiện'),
(N'Máy Mài Móng Tự Động Chống Trầy Xước', 135000, 10, N'Động cơ êm ái không gây hoảng sợ, giúp mài dũa móng Boss gọn gàng, tránh cào xước sofa và người Sen.', '~/images/products/31-may-mai-mong-tu-dong.jpg', N'Phụ kiện'),
(N'Đệm Ổ Nằm Hình Quả Chuối Siêu Ấm', 120000, 15, N'Kiểu dáng quả chuối bóc vỏ độc lạ đáng yêu, chất bông cotton siêu mềm mại cho Boss giấc ngủ ngon lành.', '~/images/products/32-dem-nam-hinh-qua-chuoi.jpg', N'Phụ kiện');

UPDATE Products SET Category = N'Thức ăn mèo' WHERE ProductID BETWEEN 23 AND 24;
UPDATE Products SET Category = N'Thức ăn chó' WHERE ProductID BETWEEN 25 AND 26;
UPDATE Products SET Category = N'Thức ăn mèo' WHERE ProductID = 27;
UPDATE Products SET Category = N'Phụ kiện' WHERE ProductID BETWEEN 28 AND 32;

USE PetLuvDB;
GO

--Vì web không hiện ra sản phẩm

SET IDENTITY_INSERT Products ON;


INSERT INTO Products (ProductID, ProductName, Price, Stock, Description, ImageURL, Category)
VALUES 
(23, N'Súp Thưởng Ciao Churu Vị Cá Hồi', 45000, 100, N'Súp thưởng thơm ngon kích thích vị giác tuyệt đối, bổ sung vitamin và giúp mượt lông cho mèo cưng.', '~/images/products/23-sup-thuong-ciao-ca-hoi.jpg', N'Thức ăn mèo'),
(24, N'Hạt Cho Mèo Con Orijen Cat & Kitten', 290000, 50, N'Dòng hạt cao cấp chứa 85% thành phần thịt cá tươi nguyên chất, cung cấp nguồn đạm sinh học dồi dào.', '~/images/products/24-hat-meo-orijen-kitten.jpg', N'Thức ăn mèo'),
(25, N'Pate Lon Morando Cho Chó Vị Thịt Gà', 65000, 80, N'Pate lon nhập khẩu từ Ý, kết cấu mềm mịn, giàu dinh dưỡng, thích hợp làm bữa ăn đổi vị cho cún.', '~/images/products/25-pate-lon-morando-vi-ga.jpg', N'Thức ăn chó'),
(26, N'Xương Gặm Canxi Sạch Răng Cho Cún', 35000, 120, N'Bánh thưởng dạng xương giúp cún giảm mảng bám, sạch răng thơm miệng và hạn chế cắn phá đồ đạc.', '~/images/products/26-banh-thuong-xuong-gam.jpg', N'Thức ăn chó'),
(27, N'Hạt Sỏi Thận Royal Canin Urinary S/O', 220000, 40, N'Thức ăn dinh dưỡng chuyên dụng cho mèo giúp hòa tan sỏi struvite và ngăn ngừa nguy cơ tái phát sỏi thận.', '~/images/products/27-hat-meo-royal-canin-urinary.jpg', N'Thức ăn mèo'),
(28, N'Máy Lọc Nước Tự Động Đài Phun', 250000, 15, N'Hệ thống lọc nước tuần hoàn tự động giúp kích thích các Boss uống nhiều nước hơn, bảo vệ hệ tiết niệu.', '~/images/products/28-may-loc-nuoc-tu-dong.jpg', N'Phụ kiện'),
(29, N'Balo Phi Hành Gia Trong Suốt Cho Boss', 180000, 30, N'Thiết kế kính vòm trong suốt thời trang giúp Boss dễ dàng ngắm nhìn thế giới khi cùng Sen đi dạo phố.', '~/images/products/29-balo-phi-hanh-gia.jpg', N'Phụ kiện'),
(30, N'Xẻng Xúc Cát Vệ Sinh Nhựa PP Dày', 15000, 200, N'Chất liệu nhựa cao cấp siêu bền, thiết kế lỗ lọc chuẩn xác giúp dọn dẹp khay cát của Boss nhanh chóng.', '~/images/products/30-xeng-xuc-cat-ve-sinh.jpg', N'Phụ kiện'),
(31, N'Máy Mài Móng Tự Động Chống Trầy Xước', 135000, 25, N'Động cơ êm ái không gây hoảng sợ, giúp mài dũa móng Boss gọn gàng, tránh cào xước sofa và người Sen.', '~/images/products/31-may-mai-mong-tu-dong.jpg', N'Phụ kiện'),
(32, N'Đệm Ổ Nằm Hình Quả Chuối Siêu Ấm', 120000, 10, N'Kiểu dáng quả chuối bóc vỏ độc lạ đáng yêu, chất bông cotton siêu mềm mại cho Boss giấc ngủ ngon lành.', '~/images/products/32-dem-nam-hinh-qua-chuoi.jpg', N'Phụ kiện');

SET IDENTITY_INSERT Products OFF;
GO