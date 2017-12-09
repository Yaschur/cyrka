using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.domain;
using cyrka.api.domain.events;
using cyrka.api.infra.nexter;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cyrka.api.infra.stores
{
	public class MongoEventStore : IEventStore
	{
		const string CollectionKeyName = "events";

		public MongoEventStore(IMongoDatabase mongoDatabase)
		{
			_mDb = mongoDatabase;
			_eventCollection = _mDb.GetCollection<Event>(CollectionKeyName);
		}

		public async Task<ulong> GetLastStoredId()
		{
			return (
				await _eventCollection.AsQueryable()
					.OrderByDescending(e => e.Id)
					.FirstOrDefaultAsync()
				)?
				.Id ?? 0;
		}

		public async Task<Event[]> FindAllAfterId(ulong id)
		{
			return (
				await _eventCollection.AsQueryable()
					.Where(e => e.Id > id)
					.OrderByDescending(e => e.Id)
					.ToListAsync()
				)
				.ToArray();
		}

		public async Task Store(Event @event)
		{
			await _eventCollection.InsertOneAsync(@event);
		}

		private readonly IMongoDatabase _mDb;
		private readonly IMongoCollection<Event> _eventCollection;
	}
}
