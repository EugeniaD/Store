drop proc if exists dbo.OrderDetails_Add
go

create proc dbo.OrderDetails_Add	
  @OrderId int, 
  @GoodsId int,
  @Quantity bigint,
  @LocalPrice money
as
begin
	insert into dbo.OrderDetails (OrderID, GoodsId, Quantity, LocalPrice)
	values (@OrderId , @GoodsId, @Quantity, @LocalPrice)
	SELECT  SCOPE_IDENTITY()
end 

