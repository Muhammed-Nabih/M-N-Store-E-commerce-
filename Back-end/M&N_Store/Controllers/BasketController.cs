using AutoMapper;
using M_N_Store.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Store.Domain.Interfaces;

namespace M_N_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;

        public BasketController(IUnitOfWork UOW)
        {
            _uOW = UOW;
        }

        [HttpGet("get-basket-item/{Id}")]
        public async Task<IActionResult> GetBasketById(string Id)
        {
            var _basket = await _uOW.BasketRepository.GetBasketAsync(Id);
            return Ok(_basket ?? new CustomerBasket(Id));
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
        {
            var _basket = await _uOW.BasketRepository.UpdateBasketAsync(customerBasket);

            return Ok(_basket);
        }

        [HttpDelete("delete-basket-item/{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await _uOW.BasketRepository.DeleteBasketAsync(Id));
        }

    }
}
