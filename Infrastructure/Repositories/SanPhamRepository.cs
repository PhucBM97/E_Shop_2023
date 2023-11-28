using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SanPhamRepository : GenericRepository<Sanpham>, ISanPhamRepository
    {
        public SanPhamRepository(QLShopContext dbContext) : base(dbContext)
        {
            
        }

    }
}
