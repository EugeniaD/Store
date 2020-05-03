drop proc if exists dbo.OrderDetails_GetById
go

create proc dbo.OrderDetails_GetById
@orderId int
 
as
begin
	select 
		od.Id,
		od.Quantity, 
		od.LocalPrice, 
		g.Id, 
		g.Brand,
		g.Model,
		c.Id,
		c.Name
	from dbo.OrderDetails od 
		join dbo.Goods g on g.Id = od.GoodsId
		join dbo.Category c on c.Id = g.SubcategoryId
	where od.OrderID = @orderId
end

