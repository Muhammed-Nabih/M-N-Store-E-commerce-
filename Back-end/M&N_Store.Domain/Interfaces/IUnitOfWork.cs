using M_N_Store.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Store.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get;  }
        public IProductRepository ProductRepository { get;  }
        public IBasketRepository BasketRepository { get;  }

    }
}
