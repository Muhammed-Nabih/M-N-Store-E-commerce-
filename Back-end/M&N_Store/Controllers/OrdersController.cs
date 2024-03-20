using AutoMapper;
using M_N_Store.Domain.Dtos;
using M_N_Store.Domain.Entities.Order;
using M_N_Store.Domain.Services;
using M_N_Store.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Store.Domain.Interfaces;
using System.Security.Claims;

namespace M_N_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork UOW, IOrderServices orderServices, IMapper mapper)
        {
            _uOW = UOW;
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(OrderDto orderdto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var address = _mapper.Map<AddressDto, ShipAddress>(orderdto.ShipToAddress);

            var order = await _orderServices.CreateOrderAsync(email, orderdto.DeliveryMethodId, orderdto.BasketId, address);

            if (order is null) return BadRequest(new BaseCommonResponse(400, "Error While Creating Order"));

            return Ok(order);

        }
        [HttpGet("get-orders-for-user")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrdersForUserAsync(email);
            var result = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(result);
        }
        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrderByIdAsync(id, email);
            if (order is null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(result);
        }
        [HttpGet("get-delivery-methods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            return Ok(await _orderServices.GetDeliveryMethodsAsync());
        }
    }
}
