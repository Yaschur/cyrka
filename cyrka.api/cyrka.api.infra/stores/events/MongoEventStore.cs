using System;
using System.Collections.Generic;
using System.Linq;
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
					.OrderByDescending(e => e.Id)
					.ToListAsync()
				)
				.ToArray();
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
