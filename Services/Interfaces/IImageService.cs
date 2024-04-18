using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllImages();

        Task<IEnumerable<Image>> GetIamgesByProductId(int productId);

        Task<bool> CreateImage(Image image);

        bool DeleteImage(Image image);

    }
}
