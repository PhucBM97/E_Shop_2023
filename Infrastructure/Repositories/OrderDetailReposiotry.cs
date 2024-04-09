using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderDetailReposiotry : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailReposiotry(E_ShopContext context) : base(context)
        {
        }
    }
}
