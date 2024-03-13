using M_N_Store.Core.Dtos;
using M_N_Store.Domain.Sharing;
using N_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<ReturnProductDto> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteAsyncWithPicture(int id);

    }
}
