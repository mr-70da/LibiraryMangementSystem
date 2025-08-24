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





-- Insert Authors
INSERT INTO Author (FirstName, LastName)
VALUES 
('George', 'Orwell'),
('Jane', 'Austen'),
('Mark', 'Twain'),
('J.K.', 'Rowling');

-- Insert Library Branches
INSERT INTO LibraryBranch (BranchName, [Address], ContactNumber)
VALUES 
('Central Library', '123 Main St, Downtown', '123-456-7890'),
('East Branch', '456 East Ave, Suburb', '987-654-3210'),
('West Branch', '789 West Blvd, Uptown', '555-111-2222');

-- Insert Users
INSERT INTO [User] (FirstName, LastName, Email, RegistrationDate)
VALUES
('Alice', 'Smith', 'alice.smith@example.com', '2022-01-15'),
('Bob', 'Johnson', 'bob.johnson@example.com', '2022-03-20'),
('Charlie', 'Brown', 'charlie.brown@example.com', '2023-05-10'),
('Diana', 'Prince', 'diana.prince@example.com', '2023-07-25');

-- Insert Books (make sure AuthorId and BranchId exist)
INSERT INTO Book (AuthorId, BranchId, Title, Edition, CopyRightYear, Price)
VALUES
(1, 1, '1984', 1, 1949, 15.99),
(2, 2, 'Pride and Prejudice', 3, 1813, 12.50),
(3, 3, 'Adventures of Huckleberry Finn', 2, 1884, 10.75),
(4, 1, 'Harry Potter and the Philosopher''s Stone', 1, 1997, 20.00),
(4, 2, 'Harry Potter and the Chamber of Secrets', 1, 1998, 22.00);

-- Insert Borrowing History (make sure BookId and UserId exist)
INSERT INTO BorrowingHistory (BookId, UserId, BorrowDate, ReturnDate)
VALUES
(5, 4, '2023-05-21', '2023-06-08'),
(3, 3, '2023-01-16', NULL),
(1, 3, '2023-04-29', NULL),
(5, 3, '2023-06-02', '2023-06-09'),
(4, 1, '2023-06-15', '2023-06-29'),
(5, 1, '2023-02-27', '2023-03-01'),
(1, 3, '2023-01-06', '2023-01-15'),
(3, 4, '2023-02-14', '2023-02-15'),
(1, 1, '2023-02-28', '2023-03-20'),
(3, 4, '2023-04-23', '2023-04-29'),
(2, 2, '2023-02-10', '2023-02-25'),
(4, 2, '2023-03-05', '2023-03-14'),
(5, 5, '2023-01-25', NULL),
(1, 4, '2023-03-30', '2023-04-05'),
(2, 1, '2023-05-10', '2023-05-18'),
(3, 2, '2023-04-01', NULL),
(4, 3, '2023-02-07', '2023-02-21'),
(2, 5, '2023-06-05', '2023-06-12'),
(5, 2, '2023-01-18', '2023-01-22'),
(1, 5, '2023-04-11', '2023-04-25'),
(3, 1, '2023-05-20', NULL),
(2, 3, '2023-02-03', '2023-02-10'),
(4, 4, '2023-01-09', '2023-01-19'),
(5, 1, '2023-06-01', NULL),
(1, 2, '2023-03-22', '2023-03-29');

SELECT * FROM Author;
SELECT * FROM LibraryBranch;
SELECT * FROM [User];
SELECT * FROM Book;
SELECT * FROM BorrowingHistory;

--book with author name and branch name (innerjoin)
SELECT Title , BranchName ,CONCAT(a.FirstName ,' ', a.LastName) as [Author name]  From Book b , LibraryBranch lb ,Author a
where b.BranchId = lb.Id AND a.Id = b.AuthorId;



--most borrowed books
SELECT BookId ,COUNT(BookId) As bookCount
FROM BorrowingHistory
GROUP by BookId;

--borrowed book count per branch
SELECT count(*) as BookCount
FROM LibraryBranch l, Book b
Where b.BranchId = l.Id
Group BY BranchId;

