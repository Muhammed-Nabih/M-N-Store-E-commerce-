using AutoMapper;
using M_N_Store.Domain.Interfaces;
using M_N_Store.Infrastructure.Repositories;
using Microsoft.Extensions.FileProviders;
using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly IConnectionMultiplexer _redis;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IBasketRepository BasketRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper , IFileProvider fileProvider, IConnectionMultiplexer redis) {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _redis = redis;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
            BasketRepository = new BasketRepository(_redis);
        }
    }
}
