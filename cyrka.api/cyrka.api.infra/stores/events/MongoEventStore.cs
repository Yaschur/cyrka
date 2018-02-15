using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using cyrka.api.common.events;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cyrka.api.infra.stores.events
{
	public class MongoEventStore : IEventStore, IDisposable
	{
		const string CollectionKeyName = "events";

		public MongoEventStore(IMongoDatabase mongoDatabase, IEnumerable<IDbMapping> maps)
		{
			foreach (var map in maps)
			{
				map.DefineMaps();
			}

			_eventsChannel = new Subject<Event>();
			_mDb = mongoDatabase;
			try
			{
				_eventsCollection = _mDb.GetCollection<Event>(CollectionKeyName);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public async Task<ulong> GetLastStoredId()
		{
			try
			{
				return (
					await _eventsCollection.AsQueryable()
						.OrderByDescending(e => e.Id)
						.FirstOrDefaultAsync()
					)?
					.Id ?? 0;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public async Task<Event[]> FindAllAfterId(ulong id)
		{
			return (
				await _eventsCollection.AsQueryable()
					.Where(e => e.Id > id)
					.OrderBy(e => e.Id)
					.ToListAsync()
			)
			.ToArray();
		}

		public async Task<Event[]> FindAllOfAggregateById<TAggregate>(string aggregateId)
			where TAggregate : class
		{
			var aggregateName = typeof(TAggregate).Name;
			var query = _eventsCollection.AsQueryable()
				.Where(e => e.EventData.AggregateType == aggregateName && e.EventData.AggregateId == aggregateId);
			return (await query.OrderBy(e => e.Id).ToListAsync()).ToArray();
		}

		public async Task<Event[]> FindLastNWithDataOf<TEventData>(int n, Expression<Func<Event, bool>> eventDataPredicate = null)
			where TEventData : EventData
		{
			var query = _eventsCollection.AsQueryable()
				.Where(e => e.EventData is TEventData);
			if (eventDataPredicate != null)
				query = query.Where(eventDataPredicate);
			return (await query.OrderByDescending(e => e.Id).Take(n).ToListAsync()).ToArray();
		}
		public async Task Store(Event @event)
		{
			try
			{
				await _eventsCollection.InsertOneAsync(@event);
				_eventsChannel.OnNext(@event);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IObservable<Event> AsObservable()
		{
			return _eventsChannel.AsObservable();
		}

		public void Dispose()
		{
			if (_eventsChannel != null && !_eventsChannel.IsDisposed)
			{
				_eventsChannel.OnCompleted();
				_eventsChannel.Dispose();
			}
		}

		private readonly IMongoDatabase _mDb;
		private readonly IMongoCollection<Event> _eventsCollection;
		private readonly Subject<Event> _eventsChannel;
	}
}
