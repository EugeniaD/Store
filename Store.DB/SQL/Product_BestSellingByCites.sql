
drop proc if exists dbo.Product_BestSellingByCites
go

create proc [dbo].[Product_BestSellingByCites]	
as
begin	
	SELECT w.Id, w.[Name], 
	(SELECT TOP 1 g.Id 
	FROM dbo.Goods g
				inner join dbo.OrderDetails od on od.GoodsId = g.Id
				inner join dbo.[Order] o on od.OrderID = o.Id
				inner join dbo.Warehouse w1 on w.Id= o.WarehouseId
				where w.Id= o.WarehouseId
		 group by g.Id
		 order by sum(od.Quantity)desc ) goodsId
	FROM dbo.Warehouse w
	where w.Id != 5
end 
