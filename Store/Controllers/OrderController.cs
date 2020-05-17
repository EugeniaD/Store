using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.API.Models.InputModels;
using Store.DB.Models;
using AutoMapper;
using Store.API.Models.OutputModels;
using StoreRepository.Repositories;
using Store.Core.Enums;

namespace Store.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }


        // POST: api/order
        [HttpPost] 
        public async ValueTask<ActionResult<OrderOutputModel>> AddOrder(OrderInputModel inputModel)
        {
            if(inputModel.WarehouseId < (int)WarehouseEnum.SPb || inputModel.WarehouseId > (int)WarehouseEnum.Storage)
            {
                return BadRequest($"Warehouse Id must be greater than {(int)WarehouseEnum.SPb} and less then {(int)WarehouseEnum.Storage}");
            }

            var result = await _orderRepository.AddOrder(_mapper.Map<Order>(inputModel));
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Order not added");
                return Ok(_mapper.Map<OrderOutputModel>(result.RequestData));
            }
            return Problem($"Adding order failed {result.ExMessage}", statusCode: 520);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")] 
        public async ValueTask<ActionResult<OrderOutputModel>> GetOrderWithDetails(int id)
        {
            if (id < 1) return BadRequest("Id must be greater than 1");
            var result = await _orderRepository.GetOrderWithDetailsById(id);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Order not found");
                return Ok(_mapper.Map<OrderOutputModel>(result.RequestData));
            }
            return Problem($"Geting order failed {result.ExMessage}", statusCode: 520);
        }

        // GET: api/order/between-dates
        [HttpGet("between-dates")] 
        public async ValueTask<ActionResult<List<OrdersInTimePeriodOutputModel>>> GetOrdersByDates(ByDatesInputModel inputModel)
        {
            DateTime fromDate = Convert.ToDateTime(inputModel.FromDate);
            DateTime toDate = Convert.ToDateTime(inputModel.ToDate);

            var result = await _orderRepository.GetOrdersByDates(fromDate, toDate);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Orders not found");
                return Ok(_mapper.Map<List<OrdersInTimePeriodOutputModel>>(result.RequestData));
            }
            return Problem($"Geting orders failed {result.ExMessage}", statusCode: 520);
        }

        // GET: api/order/total-cost-by-warehouse
        [HttpGet("total-cost-by-warehouse")]
        public async ValueTask<ActionResult<List<WarehouseTotalCostOutputModel>>> GetTotalCostByWarehouse()
        {
            var result = await _orderRepository.GetTotalCostByWarehouse();
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Something went wrong");
                return Ok(_mapper.Map<List<WarehouseTotalCostOutputModel>>(result.RequestData));
            }
            return Problem($"Geting total cost failed {result.ExMessage}", statusCode: 520);
        }

        // GET: api/Order/total-cost-by-isforeign 
        [HttpGet("total-cost-by-isforeign")]
        public async ValueTask<ActionResult<SalesByIsForeignOutputModel>> GetTotalCostByIsForeign()
        {
            var result = await _orderRepository.GetTotalCostByIsForeign();
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Something went wrong");
                return Ok(_mapper.Map<SalesByIsForeignOutputModel>(result.RequestData));
            }
            return Problem($"Geting total cost failed {result.ExMessage}", statusCode: 520);
        }
    }
}
