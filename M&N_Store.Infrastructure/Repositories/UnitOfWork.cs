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
        private readonly ApplicationDbContext context;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context) {
            this.context = context;
            CategoryRepository = new CategoryRepository(context);
            ProductRepository = new ProductRepository(context);
        }
    }
}
