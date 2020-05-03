drop proc if exists dbo.Goods_AddMany
go

create proc [dbo].[Goods_AddMany]	

as
begin 
	declare	@Price money,
			@Brand nvarchar(100),
			@Model nvarchar(100),
			@CategoryId int,
			@SubcategoryId int,
			@GoogsId int,
			@WarehouseId int,
			@Quantity int,
			@WarehouseId—ounter int,
			@—ounter int

	set @—ounter = 0;

	while @—ounter < 500000
	begin

		set @Price = (select (ROUND(1000+(RAND(CHECKSUM(NEWID()))*100000),0)) )
		set @Brand = (select top 1 [Brand] from dbo.Goods where  Id < 34 order by newid())
		set @Model = (select CONVERT(nvarchar(100),LEFT(NEWID(),20)))
		set @CategoryId = (select top 1 [id] from dbo.Category where ParentId is null order by newid())
		set @SubcategoryId = (select top 1 [id] from dbo.Category where ParentId= @CategoryId order by newid())

		set @WarehouseId—ounter = (select (ROUND(1+(RAND(CHECKSUM(NEWID()))*5),0)))

		insert into [dbo].[Goods]
				   ([Price],
				   [Brand],
				   [Model],
				   [CategoryId],
				   [SubcategoryId])
				 values
					   (@Price,
					   @Brand,
					   @Model,
					   @CategoryId,
					   @SubcategoryId)		  
				 set @GoogsId = SCOPE_IDENTITY()

		while @WarehouseId—ounter > 0
		begin
			set @WarehouseId = (select (ROUND(1 +(RAND(CHECKSUM(NEWID()))*5),0)))
			set @Quantity = (select (ROUND(0 +(RAND(CHECKSUM(NEWID()))*500),0)) )

			if @WarehouseId != 6
			insert into [dbo].[Goods_Storege]
					   ([GoodsId], 
					   [WarehouseId],
					   [Quantity])
					 values
						   (@GoogsId,
						   @WarehouseId,
						   @Quantity)	
			set @WarehouseId—ounter = @WarehouseId—ounter - 1
		end;
		set @—ounter = @—ounter + 1
	end;
end
