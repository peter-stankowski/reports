CREATE VIEW [dbo].[CategoryName_LookupView]
	AS 

	select	[Name]	= CategoryName,
			[Value] = CategoryName
	from	dbo.Categories
