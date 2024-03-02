using AutoMapper;
using Microsoft.Extensions.FileProviders;
using N_Store.Domain.Interfaces;
using N_Store.Infrastructure.Data;
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

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper , IFileProvider fileProvider) {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
        }
    }
}
