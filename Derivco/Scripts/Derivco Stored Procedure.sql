--Stored Procedure Get Order Summary
GO
CREATE PROCEDURE pr_GetOrderSummary
	@StartDate AS datetime,
	@EndDate AS datetime,
	@CustomerID AS nchar(5),
	@EmployeeID AS int
AS
SELECT O.OrderID ,
	CONCAT(E.TitleOfCourtesy,' ',E.FirstName,' ', E.LastName) AS EmployeeFullName,
	C.CompanyName AS Customer,
	S.CompanyName AS Shipper,
	COUNT(OD.OrderID) AS NumberOfOrders,
	FORMAT(O.OrderDate, 'dd-MM-yyyy') AS Date,
	FORMAT(O.Freight,'C', 'af-ZA') AS TotalFreightCost,
	COUNT(P.ProductName) AS NumberOfDifferentProducts,
	FORMAT(SUM(OD.UnitPrice*(1- OD.Discount)* OD.Quantity),'C', 'af-ZA') as TotalOrderValue

FROM Orders O
	INNER JOIN [Order Details] OD ON OD.OrderID = O.OrderID
	JOIN Shippers S ON O.ShipVia = S.ShipperID
	JOIN Customers C ON O.CustomerID = C.CustomerID
	JOIN Employees E ON O.EmployeeID = E.EmployeeID
	JOIN Products P ON OD.ProductID = P.ProductID
WHERE O.OrderDate BETWEEN @StartDate AND @EndDate 
	AND O.EmployeeID = ISNULL(@EmployeeID, O.EmployeeID)
	AND O.CustomerID = ISNULL(@CustomerID, O.CustomerID)
GROUP BY OD.OrderId, O.OrderID, S.CompanyName, C.CompanyName, E.TitleOfCourtesy, E.FirstName, E.LastName, O.Freight, O.OrderDate
ORDER BY O.OrderID
GO

-- Function to Drop the Stored Procedure. Uncomment to use the function
DROP PROCEDURE pr_GetOrderSummary;  
GO 

--Test Filters for the Stored Procedure. Uncomment to use the Test Queries
--exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=NULL , @CustomerID=NULL
--exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=5 , @CustomerID=NULL
--exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=NULL , @CustomerID='VINET'
--exec pr_GetOrderSummary @StartDate='1 Jan 1996', @EndDate='31 Aug 1996', @EmployeeID=5 , @CustomerID='VINET'