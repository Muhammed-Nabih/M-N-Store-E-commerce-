using M_N_Store.Domain.Entities;
using M_N_Store.Domain.Services;
using M_N_Store.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace M_N_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentsController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePayment(basketId);
            if (basket == null) return BadRequest(new BaseCommonResponse(400, "Problem With Your Basket"));

            return basket;
        }
    }
}
