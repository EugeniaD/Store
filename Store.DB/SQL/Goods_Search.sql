drop proc if exists dbo.Goods_Search
go

create proc dbo.Goods_Search	
	@Id int = null,
	@Price money = null,
	@Brand nvarchar(100) = null,
	@Model nvarchar(100) = null,
	@CategoryId int = null,
	@SubcategoryId int = null

as
begin	

declare @sql nvarchar(max)

set @sql=N'select	
		g.Id,
		g.Price,
		g.Brand,
		g.Model,
		c.Id,
		c.Name
		
		from dbo.Goods g
		inner join dbo.Category c on c.Id = g.CategoryId
		inner join dbo.Category c1 on c1.Id = g.SubcategoryId
		where '

	if @Id is not null 
	set @sql=@sql+N'g.Id=' + CAST (@Id as nvarchar)+ ' and ' 
	
	if @Price is not null
	set @sql=@sql+N'g.Price='+ CAST ( @Price as nvarchar) + ' and ' 

	if @Brand is not null
	set @sql=@sql+N'g.Brand='''+ @Brand + ''' and ' 

	if @Model is not null 
	set @sql=@sql+N'g.Model=''' + @Model + ''' and ' 

	if @CategoryId is not null 
	set @sql=@sql+N'c.Id=' + CAST (@CategoryId as nvarchar)+ ' and ' 
	
	if @SubcategoryId is not null 
	set @sql=@sql+N'c1.Id=' + CAST (@SubcategoryId as nvarchar)+ ' and ' 

	set @sql=@sql+N' 1=1'

	print @sql
	EXECUTE sp_executesql @SQL

end 

-- exec dbo.Goods_Search null, null, 'HANSA'