using System.Threading.Tasks;

namespace cyrka.api.common.commands
{
	public interface IAggregateRepository<TAggregate>
	{
		Task<TAggregate> GetById(string aggregateId);
	}
}
