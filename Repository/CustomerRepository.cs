using DataAccess.Models;
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
    }
}
