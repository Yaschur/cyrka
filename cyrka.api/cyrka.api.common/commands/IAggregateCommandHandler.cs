using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public interface IAggregateCommandHandler<TCommand, TAggregate>
	{
		Task<EventData[]> Handle(TCommand command, TAggregate aggregate);
	}
}
