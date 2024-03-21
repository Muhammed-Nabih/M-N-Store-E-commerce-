using M_N_Store.Domain.Entities;
using M_N_Store.Domain.Interfaces;
using M_N_Store.Domain.Services;
using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Infrastructure.Repositories
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _uOW;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public PaymentServices(IUnitOfWork UOW, IConfiguration config, ApplicationDbContext context)
        {
            _uOW = UOW;
            _config = config;
            _context = context;
        }

        public async Task<CustomerBasket> CreateOrUpdatePayment(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:Secretkey"];
            var basket = await _uOW.BasketRepository.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            if (basket == null) return null;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _context.DeliveryMethods.Where(x => x.Id == basket.DeliveryMethodId.Value)
                    .FirstOrDefaultAsync();
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.BasketItems)
            {
                var productItem = await _uOW.ProductRepository.GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
            var services = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                intent = await services.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                //update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,

                };
                await services.UpdateAsync(basket.PaymentIntentId, options);

            }
            await _uOW.BasketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
