using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentNameRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Ass2SignalRRazorPagesContext context;

        public IndexModel(ILogger<IndexModel> logger, Ass2SignalRRazorPagesContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public List<Product> Products { get; set; }
        public void OnGet()
        {
            Products = context.Products.ToList();
        }
    }
}
