using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepository
    {
        public List<Customer> GetCustomersWithDiscount(int discountRate);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task<Customer?> GetCustomerById(string id);
        Task<string> GetFinalIdCustomer();
    }
}
