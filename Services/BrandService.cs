using Core.Interfaces;
using Core.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BrandService : IBrandService
    {
        public IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var brands = await _unitOfWork.Brands.GetAll();
            return brands;
        }
    }
}
