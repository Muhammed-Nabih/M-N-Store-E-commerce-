using M_N_Store.Domain.Entities;
using M_N_Store.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_N_Store.Domain.Services
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdatePayment(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
