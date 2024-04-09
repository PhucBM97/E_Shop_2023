using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        IBrandRepository Brands { get; }

        IImageRepository Images { get; }

        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }

        IOrderDetailRepository OrderDetails { get; }


        int Save();
    }
}
