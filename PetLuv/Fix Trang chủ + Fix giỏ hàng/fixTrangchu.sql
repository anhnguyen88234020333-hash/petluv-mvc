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