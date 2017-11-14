using System.Threading.Tasks;
using cyrka.api.domain.customers;

namespace cyrka.api.domain
{
	public interface ICustomerRepository
	{
		Task Save(CustomerAggregate customer);

		Task<CustomerAggregate> GetById(string customerId);
	}
}
