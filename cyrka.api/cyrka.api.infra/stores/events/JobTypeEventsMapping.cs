using cyrka.api.domain.jobs;
using cyrka.api.domain.jobs.commands;
using cyrka.api.domain.jobs.commands.register;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace cyrka.api.infra.stores.events
{
	public class JobTypeEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<JobTypeEventData>();

			BsonClassMap.RegisterClassMap<JobTypeRegistered>(cm =>
			{
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapField(cr => cr.Unit)
					.SetSerializer(new EnumSerializer<JobTypeUnit>(BsonType.String));
				cm.MapField(cr => cr.Rate);
				cm.MapCreator(cr => new JobTypeRegistered(cr.AggregateId, cr.Name, cr.Description, cr.Unit, cr.Rate));
			});
		}
	}
}
