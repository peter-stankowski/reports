CREATE VIEW [dbo].[CustomerID_LookupView]
	AS 
	
	select	[Name]	= CompanyName,
			[Value] = CustomerID
	from	dbo.customers
