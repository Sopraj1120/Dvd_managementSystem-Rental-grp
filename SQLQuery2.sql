create database DvdRental;
use DvdRental;

CREATE TABLE RentalRequests (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MovieId INT NOT NULL,
    CustomerId INT NOT NULL,
    RentDate DATETIME NOT NULL,
    ReturnDate DATETIME NOT NULL,
    MovieImage VARBINARY(MAX), 
    MovieImageType NVARCHAR(50)
);
