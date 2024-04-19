using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class Constants
    {
        public enum OrderStatus
        {
            Cancel = 0,
            Pending = 1,
            Done = 2
        }
        public enum Payment
        {
            Offline = 1,
            Online = 2,
        }
    }
}
