using Microsoft.AspNetCore.Mvc;
using Store.API.Models.InputModels;
using Store.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.API.Controllers
{
    public interface IOrderController
    {
        ValueTask<ActionResult<OrderOutputModel>> AddOrder(OrderInputModel inputModel);
        ValueTask<ActionResult<OrderOutputModel>> GetOrderWithDetails(int id);
        ValueTask<ActionResult<List<OrdersInTimePeriodOutputModel>>> GetOrdersByDates(ByDatesInputModel inputModel);
        ValueTask<ActionResult<List<WarehouseTotalCostOutputModel>>> GetTotalCostByWarehouse();
        ValueTask<ActionResult<SalesByIsForeignOutputModel>> GetTotalCostByIsForeign();
    }
}