drop proc if exists dbo.Order_WithDetails_SelectById
go

create proc dbo.Order_WithDetails_SelectById	
  @Id int
as
begin
	select 
		o.Id,
		o.[Date],
		w.Id,
		w.[Name],
		od.Id,
		od.OrderID,
		od.Quantity, 
		od.LocalPrice, 
		g.Id, 
		g.Brand,
		g.Model,
		c.Id,
		c.[Name]
from dbo.[Order] o
	join dbo.Warehouse w on w.Id = o.WarehouseId
	left join dbo.OrderDetails od on od.OrderID = o.Id
	join dbo.Goods g on g.Id = od.GoodsId
	join dbo.Category c on c.Id = g.SubcategoryId
where o.Id = @Id
end 

