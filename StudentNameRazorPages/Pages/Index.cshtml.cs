using System.ComponentModel.DataAnnotations;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace StudentNameRazorPages.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ICustomerService customerService;

		public IndexModel(ICustomerService customerService)
		{
			this.customerService = customerService;
		}
		public List<Customer> Customers { get; set; }
		[BindProperty(SupportsGet = true)]
		[Range(0, 60, ErrorMessage = "Discount percentage must be between 0 - 60.")]
		public int DiscountRate { get; set; }
		public void OnGet()
		{
			Customers = customerService.GetCustomersWithDiscount(DiscountRate);
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
