CREATE DATABASE Library;


CREATE TABLE Author(
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName varchar(25) NOT NULL,
	LastName varchar(25),
	
);
go
CREATE TABLE Book(
	Isbn INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AuthorId int FOREIGN KEY REFERENCES Author(Id), 
	BranchId int FOREIGN KEY REFERENCES LibraryBranch(Id),
	Title VARCHAR(150) NOT NULL,
	Edition INT NOT NULL,
	CopyRightYear SMALLINT,/*As int is a waste of space for Number of 4 digits*/
	Price DEC NOT NULL

);
CREATE TABLE LibraryBranch(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	BranchName VARCHAR(150),
	[Address] VARCHAR(300),
	ContactNumber VARCHAR(20),

);

CREATE TABLE BorrowingHistory(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	BookId INT FOREIGN KEY REFERENCES Book(Isbn),
	UserId INT FOREIGN KEY REFERENCES [User](Id),
	BorrowDate DATE Not NULL,
	ReturnDate DATE

);
CREATE TABLE [User](
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName VARCHAR(200),
	LastName VARCHAR(200),
	Email VARCHAR(200) NOT NULL,
	RegistrationDate DATE
);





INSERT INTO Author
VALUES ('Mahmoud','Ayman'),('Mahmoud','Ramadan'),('Ahmad','Ayman'), ('Ali','Ehab');

INSERT INTO dbo.Book (Title, Edition, CopyRightYear, Price)
VALUES
('The Pragmatic Programmer', 2, 2019, 42.50),
('Clean Code', 1, 2008, 37.99),
('Design Patterns: Elements of Reusable Object-Oriented Software', 1, 1994, 55.00),
('Introduction to Algorithms', 4, 2022, 88.75),
('Head First Java', 3, 2017, 32.40),
('Effective Java', 3, 2018, 40.25),
('SQL Server Internals', 2, 2016, 47.95),
('JavaScript: The Good Parts', 1, 2008, 25.50),
('Python Crash Course', 2, 2019, 29.99),
('Clean Architecture', 1, 2017, 36.70);

INSERT INTO Authors_of_books
VALUES
(1,2),
(1,3),
(4,6),
(1,1),
(4,2),
(2,4),
(3,3),
(1,10),
(1,9),
(2,5),
(3,9),
(4,8),
(4,7)


DROP TABLE IF EXISTS dbo.Author;
GO
DROP TABLE IF EXISTS dbo.Book;
GO
DROP TABLE IF EXISTS dbo.Authors_of_books;
GO


SELECT * FROM Author
SELECT * FROM Book
SELECt * FROM Authors_of_books



