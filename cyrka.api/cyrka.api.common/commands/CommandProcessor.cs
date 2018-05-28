using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.errors;
using cyrka.api.common.events;
using cyrka.api.common.generators;

namespace cyrka.api.common.commands
{
	public class CommandProcessor<TCommand, TAggregate>
		where TAggregate : class, new()
	{
		public CommandProcessor(
			IAggregateCommandHandler<TCommand, TAggregate> commandHandler,
			IAggregateRepository<TAggregate> aggregateRepository,
			IEventStore eventStore,
			NexterGenerator nexterGenerator
		)
		{
			_aggregateRepository = aggregateRepository;
			_eventStore = eventStore;
			_nexterGenerator = nexterGenerator;
			_commandHandler = commandHandler;
		}

		public async Task<ProcessedCommandResult> ProcessCommand(TCommand command, string aggregateId = null)
		{
			var aggregate = aggregateId != null ? await _aggregateRepository.GetById(aggregateId) : new TAggregate();
			if (aggregate == null)
				return new ProcessedCommandResult(GeneralErrors.NotFoundError);

			EventData[] handledData;
			try
			{
				handledData = await _commandHandler.Handle(command, aggregate);
			}
			catch (CodedException ex)
			{
				return new ProcessedCommandResult(ex);
			}

			var events = await Task.WhenAll(
				handledData.Select(async ed =>
				{
					var lastEventId = await _eventStore.GetLastStoredId();
					var newEventId = await _nexterGenerator.GetNextNumber(_eventStore.NexterChannelKey, lastEventId);
					var createdAt = DateTime.UtcNow;
					var newEvent = new Event(newEventId, createdAt, ed);
					return newEvent;
				}));

			foreach (var ev in events)
				await _eventStore.Store(ev);

			return new ProcessedCommandResult(events);
		}

		private readonly IAggregateRepository<TAggregate> _aggregateRepository;
		private readonly NexterGenerator _nexterGenerator;
		private readonly IEventStore _eventStore;
		private readonly IAggregateCommandHandler<TCommand, TAggregate> _commandHandler;
	}
}
