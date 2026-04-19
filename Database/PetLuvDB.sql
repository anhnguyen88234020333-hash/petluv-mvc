
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
(N'Thức ăn cho chó Pedigree', 120000, 15, N'Thức ăn dinh dưỡng cho chó trưởng thành', '~/images/products/pedigree.jpg', N'Thức ăn'),
(N'Sữa tắm cho mèo', 85000, 20, N'Sữa tắm giúp lông mèo mềm mượt', '~/images/products/cat-shampoo.jpg', N'Spa'),
(N'Pate cho mèo Whiskas', 25000, 30, N'Pate vị cá ngừ dành cho mèo', '~/images/products/whiskas-pate.jpg', N'Thức ăn');

