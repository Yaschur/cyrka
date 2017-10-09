using System.Threading.Tasks;

namespace cyrka.api.domain.customers.services
{
	public interface ICustomerRepository
	{
		Task Save(Customer customer);

		Customer GetById(string customerId);
	}
}