﻿using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICustomerService
    {
        List<Customer> GetCustomersWithDiscount(int discountRate);
        Task CreateCustomerAsync(Customer customer);
        Task EditCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(string id);
        Task AddRangeCustomersWithXMLOrJson(IFormFile file);
        Task DeleteCustomer(string id);
    }
}
