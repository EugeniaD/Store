DROP PROC IF EXISTS dbo.Product_NoOneWants
GO

CREATE PROC dbo.Product_NoOneWants	
AS
BEGIN
	SELECT 
		c.id,
		c.[Name],
		g.Id, 
		g.Brand, 
		g.Model
	FROM dbo.Goods g
	left join dbo.Category c on g.SubcategoryId = c.Id	
	left join dbo.OrderDetails od on od.GoodsId = g.Id
	WHERE od.Id is null
	GROUP BY c.[Name], c.Id, g.Id, g.Model
END
