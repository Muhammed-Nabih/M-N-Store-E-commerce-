using AutoMapper;
using M_N_Store.Core.Dtos;
using M_N_Store.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Store.Domain.Entities;
using N_Store.Domain.Interfaces;

namespace M_N_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork UOW , IMapper mapper )
        {
            _uOW = UOW;
            _mapper = mapper;
        }


        [HttpGet("get-all-products")]
        public async Task<ActionResult> Get(string sort, int? categoryId)
        {
            //var src = await _uOW.ProductRepository.GetAllAsync(x=>x.Category);
            var src = await _uOW.ProductRepository.GetAllAsync(sort,categoryId);
            var result = _mapper.Map<List<ProductDto>>(src); 
            return Ok(result);
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var src = await _uOW.ProductRepository.GetByIdAsync(id, x=> x.Category);
            if (src == null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<ProductDto>(src);
            return Ok(result);
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post(CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uOW.ProductRepository.AddAsync(productDto);
                    return res? Ok(productDto) : BadRequest(); 
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-existing-product/{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDto productdto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var res = await _uOW.ProductRepository.UpdateAsync(id, productdto);
                    return res ? Ok(res) : BadRequest(productdto);
                }
                return BadRequest(productdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("delete-existing-product/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uOW.ProductRepository.DeleteAsyncWithPicture(id);
                    return res ? Ok(res) : BadRequest();
                }
                return NotFound($"This id : {id} Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
