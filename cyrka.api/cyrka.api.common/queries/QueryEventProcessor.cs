using System;
using System.Reactive.Linq;
using cyrka.api.common.events;

namespace cyrka.api.common.queries
{
	public class QueryEventProcessor
	{
		public QueryEventProcessor(IEventStore eventStore, IQueryStore queryStore)
		{
			_eventStore = eventStore;
			_queryStore = queryStore;
		}

		public void RegisterEventProcessing<TEvent, TQueryModel>(Func<TEvent, TQueryModel, TQueryModel> procFunc)
		{
			// TODO: manage subscribtions
			_eventStore.AsObservable()
				.Where(e => e is TEvent)
				.Subscribe(e =>
				{

				});
		}

		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
	}
}
