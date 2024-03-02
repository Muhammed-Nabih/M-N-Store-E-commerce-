﻿using AutoMapper;
using M_N_Store.Core.Dtos;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using N_Store.Domain.Entities;
using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;


        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;

        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(string sort, int? categoryId)
        {
            var query = await _context.Products
                .Include(x=>x.Category)
                .AsNoTracking()
                .ToListAsync();

            // Search By CategoryId
            if (categoryId.HasValue)
            {
                query = query.Where(x=>x.CategoryId == categoryId.Value).ToList();
            }
            // Sorting
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsync":
                        query = query.OrderBy(x => x.Price).ToList(); break;
                    case "PriceDesc":
                        query = query.OrderByDescending(x => x.Price).ToList(); break;
                    case "name":
                    default:
                        query = query.OrderBy(x => x.Name).ToList();
                        break;
                }
            }

            var _result = _mapper.Map<List<ProductDto>>(query);
            return _result;
        }

        /************************************************* ADD IMAGE ****************************************************/

        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            var src = "";
            if (dto.Image != null)
            {
                var root = "/images/products";
                var productName = $"{Guid.NewGuid()}" + dto.Image.FileName;
                if (!Directory.Exists("wwwroot" + root))
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }
                src = root + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }

            }
            // Create New Product

            var res = _mapper.Map<Product>(dto);
            res.ProductPicture = src;
            await _context.Products.AddAsync(res);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct is not null)
            {
                var root = "/images/products";
                var productName = $"{Guid.NewGuid()}" + dto.Image.FileName;
                if (!Directory.Exists("wwwroot" + root))
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }
                var src = root + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(fileStream);
                }




                //remove old picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    //delete old picture
                    var oldFilePath = _fileProvider.GetFileInfo(currentProduct.ProductPicture).PhysicalPath;
                    if (File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                currentProduct.ProductPicture = src;
                //update product
                _mapper.Map(dto, currentProduct);
                await _context.SaveChangesAsync();
                return true;
            }


            return false;

        }

        public async Task<bool> DeleteAsyncWithPicture(int id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct != null)
            {
                //remove old picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    //delete old picture
                    var picInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var rootPath = picInfo.PhysicalPath;
                    System.IO.File.Delete(rootPath);
                }
                //remove
                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
