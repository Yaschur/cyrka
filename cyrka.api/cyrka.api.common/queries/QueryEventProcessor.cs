using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using cyrka.api.common.events;

namespace cyrka.api.common.queries
{
	public class QueryEventProcessor : IDisposable
	{
		public QueryEventProcessor(IEventStore eventStore, IQueryStore queryStore)
		{
			_eventStore = eventStore;
			_queryStore = queryStore;
			_subscriptions = new List<IDisposable>();
		}

		public void RegisterEventProcessing<TEventData, TQueryModel>(
			Func<TEventData, TQueryModel, TQueryModel> procFunc,
			Func<TEventData, Expression<Func<TQueryModel, bool>>> idPredicateFunc
		)
			where TEventData : EventData
			where TQueryModel : class
		{
			var newSubs = _eventStore.AsObservable()
				.Where(e => e.EventData is TEventData)
				.Subscribe(e =>
				{
					var eData = e.EventData as TEventData;
					var idPredicate = idPredicateFunc(eData);
					var queryObject = _queryStore.AsQueryable<TQueryModel>()
						.FirstOrDefault(idPredicate);
					var resultObject = procFunc(eData, queryObject);
					if (resultObject != default(TQueryModel))
						_queryStore.Upsert(resultObject, idPredicate);
				});

			_subscriptions.Add(newSubs);
		}

		public void Dispose()
		{
			_subscriptions.ForEach(s => s.Dispose());
		}

		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
		private readonly List<IDisposable> _subscriptions;
	}
}
