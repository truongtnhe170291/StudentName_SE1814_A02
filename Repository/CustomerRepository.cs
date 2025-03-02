using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Ass2SignalRRazorPagesContext _context;

        public CustomerRepository(Ass2SignalRRazorPagesContext context)
        {
            _context = context;
        }
        public List<Customer> GetCustomersWithDiscount(int discountRate)
        {
            if (discountRate <= 0 || discountRate > 60)
            {
                return _context.Customers.ToList();
            }
            return _context.Customers
                .Where(c => c.DiscountRate <= discountRate)
                .ToList();
        }
        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerById(string id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<string> GetFinalIdCustomer()
        {
            var Custom = await _context.Customers.OrderByDescending(c => c.CustomerId).FirstOrDefaultAsync();
            if (Custom == null)
                return "1";
            return Custom.CustomerId;
        }

		public async Task DeleteCustomer(string id)
		{
            var Customer = await GetCustomerById(id);
            if (Customer != null)
            {
                _context.Customers.Remove(Customer);
				await _context.SaveChangesAsync();
			}
		}
	}
}
