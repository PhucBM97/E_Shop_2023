using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ObjectWithImagesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFileCollection Images { get; set; }
    }
}
