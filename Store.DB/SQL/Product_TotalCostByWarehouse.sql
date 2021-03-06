drop proc if exists dbo.Product_TotalCostByWarehouse
go

create proc [dbo].Product_TotalCostByWarehouse
AS
BEGIN
	SELECT sum (g.Price*gs.Quantity) TotalMoney, w.Id, w.[Name]
	FROM dbo.Goods g
	inner join dbo.Goods_Storege gs on gs.GoodsId = g.Id
	inner join dbo.Warehouse w on gs.WarehouseId=w.Id
	group by  w.Id, w.[Name]
END