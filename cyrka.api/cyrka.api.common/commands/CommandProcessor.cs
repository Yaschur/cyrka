using cyrka.api.common.events;
using cyrka.api.common.generators;

namespace cyrka.api.common.commands
{
	public class CommandProcessor<TAggregate>
		where TAggregate : class
	{
		public CommandProcessor(IAggregateRepository<TAggregate> aggregateRepository, IEventStore eventStore, NexterGenerator nexterGenerator)
		{
			_aggregateRepository = aggregateRepository;
			_eventStore = eventStore;
			_nexterGenerator = nexterGenerator;
		}

		private readonly IAggregateRepository<TAggregate> _aggregateRepository;
		private readonly NexterGenerator _nexterGenerator;
		private readonly IEventStore _eventStore;
	}
}
