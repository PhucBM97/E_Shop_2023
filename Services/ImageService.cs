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
    public class ImageService : IImageService
    {
        public IUnitOfWork _unitOfWork;
        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Image>> GetAllImages()
        {
            var images = await _unitOfWork.Images.GetAll();
            return images;
        }
    }
}
