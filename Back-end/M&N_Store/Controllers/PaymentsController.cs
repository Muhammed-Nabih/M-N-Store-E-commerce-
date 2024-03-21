using M_N_Store.Domain.Entities;
using M_N_Store.Domain.Entities.Order;
using M_N_Store.Domain.Services;
using M_N_Store.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace M_N_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger<PaymentsController> _logger;
        private const string endpointSecret = "whsec_a88641e4c6a382a1e72cfc6d3207508b5b5174a08d6445648e6255144aebe199";


        public PaymentsController(IPaymentServices paymentServices,ILogger<PaymentsController> logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePayment(basketId);
            if (basket == null) return BadRequest(new BaseCommonResponse(400, "Problem With Your Basket"));

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                // Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);

                PaymentIntent intent;
                
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentPaymentFailed:
                        intent =(PaymentIntent) stripeEvent.Data.Object;
                        _logger.LogInformation("Payment Faild", intent.Id);
                        await _paymentServices.UpdateOrderPaymentFailed(intent.Id);
                        _logger.LogInformation("Updated Order Status To Failed");
                        break;
                    case Events.PaymentIntentSucceeded:
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment Succeeded", intent.Id);
                        await _paymentServices.UpdateOrderPaymentSucceeded(intent.Id);
                        _logger.LogInformation("Updated Order Status To Succeeded");
                        break;
                    
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
