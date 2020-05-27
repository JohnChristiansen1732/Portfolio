-- Create table
USE BookmarkingPageCount;

IF OBJECT_ID('dbo.PageCount', 'U') iS NOT NULL
	DROP TABLE dbo.PageCount

Create Table PageCount
(
	pk uniqueidentifier NOT NULL DEFAULT newid(),
	batchName varchar(255) NOT NULL,
	fileName varchar(255) NOT NULL,
	originalPageCount int NOT NULL,
	bookmarkedPageCount int,
	loanNumber varchar(255) NOT NULL,
	dateUpload smalldatetime,
	dateDownload smalldatetime,
	PRIMARY KEY (pk),
	CONSTRAINT uniqueImage UNIQUE(batchName, fileName, loanNumber)
);

-- Procedure to insert new file
if(OBJECT_ID('dbo.uspPageCountInsert') IS NOT NULL)
	DROP PROCEDURE dbo.uspPageCountInsert
GO
CREATE PROCEDURE dbo.uspPageCountInsert
	@batchName				varchar(255),
	@fileName				varchar(255),
	@originalPageCount		int,
	@loanNumber				varchar(255),
	@dateUpload				smalldatetime
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.PageCount
					WHERE batchName=@batchName
					AND fileName=@fileName
					AND loanNumber=@loanNumber)

	INSERT INTO dbo.PageCount
	(
		batchName,
		fileName,
		originalPageCount,
		loanNumber,
		dateUpload
	)
	VALUES
	(
		@batchName,
		@fileName,
		@originalPageCount,
		@loanNumber,
		@dateUpload
	)
END

GO

-- Procedure to select all files within a batch
if (OBJECT_ID('dbo.uspPageCountSelectByBatch') IS NOT NULL)
	DROP PROCEDURE uspPageCountSelectByBatch
GO
CREATE PROCEDURE dbo.uspPageCountSelectByBatch
	@batchName		varchar(255)
AS
BEGIN
	SELECT * FROM dbo.PageCount
	WHERE batchName=@batchName
END
GO

-- Procedure to update file with bookmarkedPageCount and download date
if (OBJECT_ID('dbo.uspPageCountUpdateBookmarkedPageCount') IS NOT NULL)
	DROP PROCEDURE dbo.uspPageCountUpdateBookmarkedPageCount
GO
CREATE PROCEDURE dbo.uspPageCountUpdateBookmarkedPageCount
	@bookmarkPageCount		int,
	@dateDownload			smalldatetime,
	@fileName				varchar(255),
	@batchName				varchar(255),
	@loanNumber				varchar(255)
AS
BEGIN
	UPDATE dbo.PageCount
	SET
		bookmarkedPageCount=@bookmarkPageCount,
		dateDownload=@dateDownload
	WHERE
		fileName=@fileName AND
		batchName=@batchName AND
		loanNumber=@loanNumber AND
		( bookmarkedPageCount<>@bookmarkPageCount OR bookmarkedPageCount is null)
END
GO