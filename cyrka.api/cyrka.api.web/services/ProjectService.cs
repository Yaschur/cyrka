using System;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.generators;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setProduct;

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
			var eventData = await handler.Handle(command);
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(SetProduct command)
		{
			var handler = new SetProductHandler(_projectRepository);
			var eventData = await handler.Handle(command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(SetJob command)
		{
			var handler = new SetJobHandler(_projectRepository);
			var eventData = await handler.Handle(command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly ProjectAggregateRepository _projectRepository;

		private async Task HandleEventData(EventData eventData)
		{
			var lastEventId = await _eventStore.GetLastStoredId();
			var newEventId = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
			var createdAt = DateTime.UtcNow;
			var newEvent = new Event(newEventId, createdAt, eventData);
			await _eventStore.Store(newEvent);
		}
	}

}
