drop proc if exists dbo.Order_ByDates
go

create proc dbo.Order_ByDates
	@FromDate date,
	@ToDate date
AS
BEGIN
	SELECT 
		o.id, 
		o.[Date], 
		w.id, 
		w.[Name], 
		sum (od.Quantity) 'Total Quantity', 
		sum(g.Price* od.Quantity) Total, 
		g.id, 
		g.Brand, 
		g.Model, 
		c.id,
		c.[Name]
	FROM dbo.[Order] o
		inner join dbo.OrderDetails od on od.OrderID = o.Id 
		inner join dbo.Warehouse w on w.Id = o.WarehouseId 
		inner join dbo.Goods g on od.GoodsId = g.Id
		inner join dbo.Category c on g.SubcategoryId=c.Id
	WHERE o.Date >  @FromDate and o.Date < @ToDate
	GROUP BY o.[Date],o.id, w.id, w.[Name], g.id, g.Brand, g.Model, c.id, c.[Name]
	ORDER BY o.[Date] asc
END