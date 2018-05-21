using System;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.common.generators;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setPayments;
using cyrka.api.domain.projects.commands.setProduct;
using cyrka.api.domain.projects.commands.setStatus;

namespace cyrka.api.web.services
{
	public class ProjectService
	{
		const string ProjectResourceKey = "projects";

		public ProjectService(CommandProcessor<ProjectAggregate> commandProcessor)
		{
			_commandProcessor = commandProcessor;
		}

		public async Task<WebAnswerBody> Do(RegisterProject command)
		{
			var result = _commandProcessor.ProcessCommand(command);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(string projectId, SetProduct command)
		{
			var handler = new SetProductHandler(_projectRepository);
			var eventData = await handler.Handle(projectId, command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(string projectId, SetJob command)
		{
			var handler = new SetJobHandler(_projectRepository);
			var eventData = await handler.Handle(projectId, command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(string projectId, ChangeJob command)
		{
			var handler = new ChangeJobHandler(_projectRepository);
			var eventData = await handler.Handle(projectId, command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(string projectId, SetStatus command)
		{
			var handler = new SetStatusHandler(_projectRepository);
			var eventData = await handler.Handle(projectId, command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		public async Task<WebAnswerBody> Do(string projectId, SetPayments command)
		{
			var handler = new SetPaymentsHandler(_projectRepository);
			var eventData = await handler.Handle(projectId, command);
			if (eventData == null)
				return null;
			await HandleEventData(eventData);

			return new WebAnswerBody
			{
				ResourceId = eventData.AggregateId,
				ResourceType = ProjectResourceKey
			};
		}

		private readonly CommandProcessor<ProjectAggregate> _commandProcessor;
	}

}
