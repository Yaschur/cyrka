using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.domain;
using cyrka.api.domain.customers.register;
using cyrka.api.domain.events;
using cyrka.api.infra.nexter;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace cyrka.api.infra.stores
{
	public class MongoEventStore : IEventStore
	{
		const string CollectionKeyName = "events";

		public MongoEventStore(IMongoDatabase mongoDatabase, IEnumerable<IDbMapping> maps)
		{
			foreach (var map in maps)
			{
				map.DefineMaps();
			}

			_mDb = mongoDatabase;
			try
			{
				_eventCollection = _mDb.GetCollection<Event>(CollectionKeyName);
			}
			catch (System.Exception e)
			{
				throw e;
			}
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
			try
			{
				await _eventCollection.InsertOneAsync(@event);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private readonly IMongoDatabase _mDb;
		private readonly IMongoCollection<Event> _eventCollection;
	}
}
