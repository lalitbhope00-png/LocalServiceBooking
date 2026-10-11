IF DB_ID(N'LocalServiceDB') IS NULL
BEGIN
    CREATE DATABASE LocalServiceDB;
END
GO
USE LocalServiceDB;
GO

IF OBJECT_ID('dbo.Bookings','U') IS NULL
BEGIN
CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Service NVARCHAR(100) NOT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    BookingDate DATE NOT NULL,
    BookingTime TIME NOT NULL,
    Description NVARCHAR(500) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    ProviderId INT NULL
);
END
GO

IF OBJECT_ID('dbo.ServiceProviders','U') IS NULL
BEGIN
CREATE TABLE ServiceProviders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProviderName NVARCHAR(100) NOT NULL,
    Service NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    Experience INT NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1
);
END
GO

IF OBJECT_ID('dbo.Reviews','U') IS NULL
BEGIN
CREATE TABLE Reviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
    Mobile NVARCHAR(15) NOT NULL,
    Rating INT NOT NULL,
    ReviewText NVARCHAR(500) NULL
);
END
GO
