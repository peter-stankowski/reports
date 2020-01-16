
CREATE PROCEDURE CustOrdersOrders @CustomerID_LookupView nchar(5)
AS
SELECT OrderID, 
	OrderDate,
	RequiredDate,
	ShippedDate
FROM Orders
WHERE CustomerID = @CustomerID_LookupView
ORDER BY OrderID
