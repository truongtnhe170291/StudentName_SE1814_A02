using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net;
using System.Numerics;

namespace StudentNameRazorPages.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly Ass2SignalRRazorPagesContext context;
		private readonly ICustomerService customerService;

		public IndexModel(ILogger<IndexModel> logger, Ass2SignalRRazorPagesContext context, ICustomerService customerService)
		{
			_logger = logger;
			this.context = context;
			this.customerService = customerService;
		}
		public List<Customer> Customers { get; set; }
		public void OnGet()
		{
			Customers = context.Customers.ToList();
		}

		public async Task<IActionResult> OnPost(IFormFile file)
		{
			await customerService.AddRangeCustomersWithXMLOrJson(file);
			return RedirectToPage();
		}
		public async Task<IActionResult> OnPostAddCustomerAsync(string CustomerName, string Address, string Phone, int DiscountRate)
		{
			if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone) || DiscountRate < 0 || DiscountRate > 100)
			{
				return Page();
			}

			var newCustomer = new Customer
			{
				CustomerName = CustomerName,
				Address = Address,
				Phone = Phone,
				DiscountRate = DiscountRate
			};

			await customerService.CreateCustomerAsync(newCustomer);

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostEditCustomerAsync(string Id, string CustomerName, string Address, string Phone, int DiscountRate)
		{
			if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone) || DiscountRate < 0 || DiscountRate > 100)
			{
				return Page();
			}

			var customer = await customerService.GetCustomerByIdAsync(Id);
			if (customer == null)
			{
				return NotFound();
			}

			customer.CustomerName = CustomerName;
			customer.Address = Address;
			customer.Phone = Phone;
			customer.DiscountRate = DiscountRate;

			await customerService.EditCustomerAsync(customer);

			return RedirectToPage();
		}

		public async Task<IActionResult> OnGetDeleteCustomer(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return Page();
			}
			await customerService.DeleteCustomer(id);
			return RedirectToPage();
		}
	}
}
