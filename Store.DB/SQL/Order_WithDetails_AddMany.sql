drop proc if exists dbo.Order_WithDetails_AddMany
go

create proc dbo.Order_WithDetails_AddMany	
   @WarehouseId int
as
begin 

	declare	 @Date datetime2(7),
			@WarehouseId int,
			@OrderID int,
			@GoodsId int,
			@Quantity int,
			@LocalPrice int,
			@Counter int,
			@GoodsCounter int,
			@GoodsAmount int

	set @counter = 0;
	set @GoodsAmount = 0;

	while @counter < 100
		begin

		set @Date = (select dateadd(day, rand(checksum(newid()))*(1+datediff(day, '2010-01-01', '2020-04-26')), '2011-01-01'))
		set @WarehouseId = (select (ROUND(1+(RAND(CHECKSUM(NEWID()))*4),0)))
		set @GoodsCounter = (select (ROUND(1+(RAND(CHECKSUM(NEWID()))*5),0)))

		insert into dbo.[Order]  ([Date], WarehouseId)
				values (@Date, @WarehouseId)			
		set @OrderID = ( select scope_identity())


		while @GoodsAmount < @GoodsCounter
		begin
			set @GoodsId = (select top 1 [Id] from dbo.Goods order by newid())
			set @Quantity = (select (ROUND(1+(RAND(CHECKSUM(NEWID()))*100),0)))
			set @LocalPrice = (select (ROUND(1000+(RAND(CHECKSUM(NEWID()))*100000),0)))

			insert into dbo.OrderDetails (OrderID, GoodsId, Quantity, LocalPrice)
					values (@OrderId , @GoodsId, @Quantity, @LocalPrice)
			set @GoodsAmount = @GoodsAmount + 1;
		end
		set @GoodsAmount = 0;
		set @Counter = @counter + 1
	end
end

