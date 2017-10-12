using System.Threading.Tasks;

namespace cyrka.api.domain.customers.services
{
	public class CustomerRepository : ICustomerRepository
	{
		public Task<Customer> GetById(string customerId)
		{
			throw new System.NotImplementedException();
		}

		public Task Save(Customer customer)
		{
			throw new System.NotImplementedException();
		}
	}
}
