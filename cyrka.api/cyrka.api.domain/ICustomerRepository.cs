using System.Threading.Tasks;
using cyrka.api.domain.customers;

namespace cyrka.api.domain
{
	public interface ICustomerRepository
	{
		Task Save(Customer customer);

		Task<Customer> GetById(string customerId);
	}
}
