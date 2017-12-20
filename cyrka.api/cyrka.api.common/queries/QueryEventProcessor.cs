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

		public void RegisterEventProcessing<TEvent, TQueryModel>(
			Func<TEvent, TQueryModel, TQueryModel> procFunc,
			Expression<Func<TQueryModel, bool>> idPredicate
		)
			where TEvent : class
			where TQueryModel : class
		{
			var newSubs = _eventStore.AsObservable()
				.Where(e => e is TEvent)
				.Subscribe(e =>
				{
					var queryObject = _queryStore.AsQueryable<TQueryModel>()
						.FirstOrDefault(idPredicate);
					var resultObject = procFunc(e as TEvent, queryObject);
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
