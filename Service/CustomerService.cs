using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Repository;
using Service.HubService;
using System.Text.Json;
using System.Xml.Serialization;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public CustomerService(ICustomerRepository repository, IHubContext<SignalrServer> hubContext)
        {
            this.repository = repository;
            this._hubContext = hubContext;
        }

        public List<Customer> GetCustomersWithDiscount(int discountRate)
        {
            return repository.GetCustomersWithDiscount(discountRate);
        }

        public async Task AddRangeCustomersWithXMLOrJson(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            List<Customer> customers = null;

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var content = await stream.ReadToEndAsync();
                if (fileExtension == ".json")
                {
                    customers = JsonSerializer.Deserialize<List<Customer>>(content);
                }
                else if (fileExtension == ".xml")
                {
                    var serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));
                    customers = (List<Customer>)serializer.Deserialize(new StringReader(content));
                }
            }

            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    await CreateCustomerAsync(customer);
                }
            }
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            int customerId;
            if(Int32.TryParse(await repository.GetFinalIdCustomer(), out customerId))
            {
                customer.CustomerId = (customerId+1).ToString();
            }
            await repository.AddCustomer(customer);
            await _hubContext.Clients.All.SendAsync("ReceiveCustomerUpdate", customer.CustomerId);
        }

		public async Task DeleteCustomer(string id)
		{
			await repository.DeleteCustomer(id);
			await _hubContext.Clients.All.SendAsync("ReceiveCustomerUpdate", id);
		}

		public async Task EditCustomerAsync(Customer customer)
        {
            await repository.UpdateCustomer(customer);
            await _hubContext.Clients.All.SendAsync("ReceiveCustomerUpdate", customer.CustomerId);
        }

        public async Task<Customer?> GetCustomerByIdAsync(string id)
        {
            var customer = await repository.GetCustomerById(id);
            await _hubContext.Clients.All.SendAsync("ReceiveDetailView", id.ToString());
            return customer;
        }
    }
}
