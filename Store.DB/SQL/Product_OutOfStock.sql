drop proc if exists dbo.Product_OutOfStock
go

create proc dbo.Product_OutOfStock
AS
BEGIN
	SELECT distinct 
		c.id,
		c.[Name],
		g.Id, 
		g.Brand, 
		g.Model
	FROM dbo.Goods g
	left join dbo.Goods_Storege gs on gs.GoodsId = g.Id
	inner join dbo.Category c on g.SubcategoryId = c.Id		
	inner join dbo.OrderDetails od on od.GoodsId = g.Id
	WHERE gs.Quantity = 0 or (gs.Id is null and od.Id is not null)  	
	GROUP BY c.id, c.[Name], g.Id, g.Brand, g.Model
END