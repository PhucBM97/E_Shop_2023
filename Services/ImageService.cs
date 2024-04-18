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

        public async Task<bool> CreateImage(Image image)
        {
            var model = await _unitOfWork.Images.Add(image);

            var result = _unitOfWork.Save();
            if (result <= 0)
                return false;

            return true;
        }

        public bool DeleteImage(Image image)
        {
            _unitOfWork.Images.Delete(image);
            var result = _unitOfWork.Save();
            if (result <= 0)
                return false;
            return true;
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            var images = await _unitOfWork.Images.GetAll();
            return images;
        }

        public async Task<IEnumerable<Image>> GetIamgesByProductId(int productId)
        {
            var model = await _unitOfWork.Images.GetDataWithPredicate(p => p.ProductId == productId);
            if (!model.Any())
                return null;
            return model;
        }
    }
}
