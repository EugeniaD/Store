DROP PROC IF EXISTS dbo.TotalSum_ByIsForeign
GO

CREATE PROC dbo.TotalSum_ByIsForeign	
AS

BEGIN
	SELECT [0] AS 'Продажи в РФ', [1] AS 'Продажи за рубежом'
	FROM (
		SELECT SUM(g.Price*od.Quantity) as total, w.isForeign
		FROM dbo.Goods g
		inner join dbo.OrderDetails od on od.GoodsId = g.Id
		inner join dbo.[Order] o on o.Id = od.OrderID
		inner join dbo.Warehouse w on w.Id = o.WarehouseId
		GROUP BY  w.isForeign) as s 
	PIVOT  
	(  
	AVG(total)  
	FOR s.isForeign IN ([0], [1])  
	) AS PivotTable; 
END
