
using M_N_Store.Domain.Entities.Order;
using M_N_Store.Domain.Services;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Infrastructure.Repositories
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _uOW;
        private readonly ApplicationDbContext _context;
        private readonly IPaymentServices _paymentServices;

        public OrderServices(IUnitOfWork UOW, ApplicationDbContext context, IPaymentServices paymentServices)
        {
            _uOW = UOW;
            _context = context;
            _paymentServices = paymentServices;

        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress)
        {
            //get basekt Item
            var basket = await _uOW.BasketRepository.GetBasketAsync(basketId);
            var items = new List<OrderItem>();

            //fill item


            foreach (var item in basket.BasketItems)
            {
                var productItem = await _uOW.ProductRepository.GetByIdAsync(item.Id);
                var productItemOrderd = new ProductItemOrderd(productItem.Id, productItem.Name, productItem.ProductPicture);
                var orderItem = new OrderItem(productItemOrderd, item.Price, item.Quantity);

                items.Add(orderItem);
            }

            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            //get delivery method
            var deliveryMethod = await _context.DeliveryMethods.Where(x => x.Id == deliveryMethodId)
                                .FirstOrDefaultAsync();
            //calculate Subtotal

            var subTotal = items.Sum(x => x.Price * x.Quantity);

            //ceck if order exisit
            var exitingOrder = await _context.Orders.Where(x => x.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync();

            if (exitingOrder is not null)
            {
                _context.Orders.Remove(exitingOrder);
                await _paymentServices.CreateOrUpdatePayment(basket.PaymentIntentId);
            }

            //initilaize on Ctor
            var order = new Order(buyerEmail, shipAddress, deliveryMethod, items, subTotal, basket.PaymentIntentId);

            //check order is not null
            if (order is null) return null;

            //adding order in Db
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            //remove Basket
            //await _uOW.BasketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => await _context.DeliveryMethods.ToListAsync();
        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _context.Orders
                    .Where(x => x.Id == id && x.BuyerEmail == buyerEmail)
                    .Include(x => x.OrderItems).ThenInclude(x => x.ProductItemOrderd)
                    .Include(x => x.DeliveryMethod)
                    .OrderByDescending(x => x.OrderDate)
                    .FirstOrDefaultAsync();
            return order;
        }
        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var order = await _context.Orders
                    .Where(x => x.BuyerEmail == buyerEmail)
                    .Include(x => x.OrderItems).ThenInclude(x => x.ProductItemOrderd)
                    .Include(x => x.DeliveryMethod)
                    .OrderByDescending(x => x.OrderDate)
                    .ToListAsync();
            return order;
        }
    }
}
