using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public abstract class AggregateCommandHandler<TCommand, TAggregate>
	{
		public abstract Task<EventData[]> Handle(TCommand command, TAggregate aggregate);
	}
}
