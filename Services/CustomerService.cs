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
    public class CustomerService : ICustomerService
    {
        public IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            if (customer is not null)
            {
                await _unitOfWork.Customers.Add(customer);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
            }
            return false;
        }

        public Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _unitOfWork.Customers.GetAll();
            return customers;
        }

        public async Task<Customer> GetCustomerByEmail(string customerEmail)
        {
            var model = await _unitOfWork.Customers.GetDataWithPredicate(p => p.Email == customerEmail);
            var customer = model.FirstOrDefault();
            if(customer is null)
                return null;
            return customer;
        }
    }
}
