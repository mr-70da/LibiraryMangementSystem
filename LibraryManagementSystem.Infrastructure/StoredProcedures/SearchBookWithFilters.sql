CREATE PROCEDURE SearchBookWithFilters
    @Title varchar(200) = NULL,
    @AuthorId int = NULL,
    @BranchId int = NULL
AS
BEGIN 
    SELECT *
    FROM Book
    WHERE (@Title IS NULL OR Title = @Title) 
        AND (@AuthorId IS NULL OR AuthorId = @AuthorId) 
        AND (@BranchId IS NULL OR BranchId = @BranchId)
END