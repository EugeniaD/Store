drop proc if exists dbo.Product_IsInStorage_NoInSPBMoscow
go

create proc dbo.Product_IsInStorage_NoInSPBMoscow	
AS
BEGIN
	SELECT 		
		c.id, 
		c.[Name],
		g.Id, 
		g.Brand,
		g.Model
  FROM dbo.Goods g
	  inner join  dbo.Goods_Storege gs on gs.GoodsId = g.Id
	  inner join dbo.Category c on c.Id = g.SubcategoryId
  WHERE gs.WarehouseId = 5 and g.Id not in
	  (SELECT g.Id 
	  FROM dbo.Goods g
		 inner join  dbo.Goods_Storege gs on gs.GoodsId = g.Id
	  WHERE gs.WarehouseId  = 1 or gs.WarehouseId  = 2)
END


	
