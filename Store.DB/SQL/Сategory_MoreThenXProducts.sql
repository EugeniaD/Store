DROP PROC IF EXISTS dbo.Сategory_MoreThenXProducts
GO

CREATE PROC dbo.Сategory_MoreThenXProducts

@amount int
AS
BEGIN	
	SELECT 
		c.Id, 
		c.Name, 
		COUNT(*) CountProducts
	FROM dbo.Goods g 
	inner join dbo.Category c on g.CategoryId = c.Id	
	GROUP BY c.Id, c.[Name]
	having COUNT(*) >= @amount
END
