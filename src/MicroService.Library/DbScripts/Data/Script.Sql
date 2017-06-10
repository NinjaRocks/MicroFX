USE [library]
GO
DELETE FROM TBookShelf
DELETE FROM TBookCheckout
DELETE FROM TBorrower
DELETE FROM TBookOnShelf
DELETE FROM TBook
GO
SET IDENTITY_INSERT TBookShelf ON
INSERT INTO [dbo].[TBookShelf]([BookShelfId],[Name]) VALUES (1, 'Default')
SET IDENTITY_INSERT TBookShelf OFF
GO
SET IDENTITY_INSERT TBorrower ON
INSERT INTO [dbo].[TBorrower]([BorrowerId], [Name]) VALUES (1, 'John Miller')
INSERT INTO [dbo].[TBorrower]([BorrowerId], [Name]) VALUES (2, 'Paul Baker')
INSERT INTO [dbo].[TBorrower]([BorrowerId], [Name]) VALUES (3, 'Alan Border')
SET IDENTITY_INSERT TBorrower OFF
GO
SET IDENTITY_INSERT TBook ON
INSERT INTO [dbo].[TBook]
           ([BookId]
           ,[Name]
           ,[ISBN])
     VALUES
           (1
           ,'A Concise History of Hong Kong'
           ,'0742534227')
SET IDENTITY_INSERT TBook OFF
GO
SET IDENTITY_INSERT TBookOnShelf ON
INSERT INTO [dbo].[TBookOnShelf]
           ([BookOnShelfId]
		   ,[BookShelfId]
           ,[BookId])
     VALUES (1,  1,	1)
SET IDENTITY_INSERT TBookOnShelf OFF
GO
SET IDENTITY_INSERT TBookCheckout ON
INSERT INTO [dbo].[TBookCheckout]
           ([BookCheckoutId]
		   , [BookOnShelfId]
           ,[Comment]
           ,[CheckedOutAt]
           ,[ReturnOn]
           ,[BorrowerId])
     VALUES (1, 1, NULL,'2017-03-08 15:19:36.760',	NULL,	1)
	 
SET IDENTITY_INSERT TBookCheckout OFF

GO


