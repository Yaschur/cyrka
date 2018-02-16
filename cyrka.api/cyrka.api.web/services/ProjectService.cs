using System;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.generators;
using cyrka.api.common.queries;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.register;

namespace cyrka.api.web.services
{
	public class ProjectService
	{
		const string EventChannelKey = "events";
		const string ProjectResourceKey = "projects";

		public ProjectService(
			NexterGenerator nexterGenerator,
			IEventStore eventStore,
			ProjectAggregateRepository projectRepository
		)
		{
			_nexter = nexterGenerator;
			_eventStore = eventStore;
			_projectRepository = projectRepository;
		}

		public async Task<WebAnswerBody> Do(RegisterProject command)
		{
			var handler = new RegisterProjectHandler(_eventStore, _nexter);
			var eventData = await handler.Handle(new RegisterProject());
			var lastEventId = await _eventStore.GetLastStoredId();
			var newEventId = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
			var createdAt = DateTime.UtcNow;
			var newEvent = new Event(newEventId, createdAt, eventData);
			await _eventStore.Store(newEvent);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly ProjectAggregateRepository _projectRepository;
	}

}
