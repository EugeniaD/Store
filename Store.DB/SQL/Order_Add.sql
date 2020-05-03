drop proc if exists dbo.Orders_Add
go

create proc dbo.Orders_Add	
   @WarehouseId int
as
begin	
	insert into dbo.[Order]  ([Date], WarehouseId)
	values (GETDATE(), @WarehouseId)
	SELECT  SCOPE_IDENTITY()
end 

