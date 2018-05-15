using System.Threading.Tasks;

namespace cyrka.api.common.commands
{
	public interface IAggregateRepository<TAggregate>
		where TAggregate : class
	{
		Task<TAggregate> GetById(string aggregateId);
	}
}
