using System.ComponentModel.DataAnnotations;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

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

        [BindProperty]
        public List<Customer> Customers { get; set; }
        [BindProperty(SupportsGet = true)]
        [Range(0, 60, ErrorMessage = "Discount percentage must be between 0 - 60.")]
        public int DiscountRate { get; set; }
        public void OnGet()
        {
            //Console.WriteLine(DiscountRate);
            //DiscountRate = 30;
            //if(DiscountRate <=0 || DiscountRate > 60)
            //{
            //    ViewData["ErrorSearch"] = "a";
            //}
            Customers = customerService.GetCustomersWithDiscount(DiscountRate);
        }
    }
}
