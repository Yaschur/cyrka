using cyrka.api.common.events;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores.events
{
	public class CoreEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<Event>(cm =>
			{
				cm.MapIdField(e => e.Id);
				cm.MapField(e => e.CreatedAt);
				cm.MapField(e => e.EventData);
				cm.MapCreator(e => new Event(e.Id, e.CreatedAt, e.EventData));
			});

			BsonClassMap.RegisterClassMap<EventData>(cm =>
			{
				cm.MapField(ed => ed.AggregateId);
				cm.MapField(ed => ed.AggregateType);
			});
		}
	}
}
