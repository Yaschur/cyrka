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

		public MongoEventStore(
			IMongoDatabase mongoDatabase,
			EventFactory eventFactory,
			NexterGenerator nexterService
		)
		{
			_nexterService = nexterService;
			_eventFactory = eventFactory;
			_mDb = mongoDatabase;
			_eventCollection = _mDb.GetCollection<EventDto>(CollectionKeyName);
		}

		public async Task<Event[]> FindAllByAggregateIdOf(string aggregateType, string aggregateId)
		{
			return (
				await _eventCollection.AsQueryable()
					.Where(e => e.AggregateType == aggregateType && e.AggregateId == aggregateId)
					.OrderByDescending(e => e.Id)
					.ToListAsync()
				)
				.Select(_eventFactory.Create)
				.ToArray();
		}

		public async Task Store(Event @event)
		{
			var lastId = await ExtractLastStoredId();
			var nextId = await _nexterService.GetNextNumber(CollectionKeyName, lastId);

			var eventDto = @event
				.GetEventDto(_eventFactory.EventDataSerializer)
				.ProvideId(nextId);

			await _eventCollection.InsertOneAsync(eventDto);
		}

		private readonly NexterGenerator _nexterService;
		private readonly IMongoDatabase _mDb;
		private readonly IMongoCollection<EventDto> _eventCollection;
		private readonly EventFactory _eventFactory;

		private async Task<ulong> ExtractLastStoredId()
		{
			return (
				await _eventCollection.AsQueryable()
					.OrderByDescending(e => e.Id)
					.FirstOrDefaultAsync())?
				.Id ?? 0;
		}
	}
}
