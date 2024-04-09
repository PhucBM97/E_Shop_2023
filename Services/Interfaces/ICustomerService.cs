using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> AddCustomer(Customer customer);

        Task<Customer> GetCustomerByEmail(string customerEmail);



    }
}
