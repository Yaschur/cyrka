using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.errors;
using cyrka.api.common.events;
using cyrka.api.common.generators;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setPayments;
using cyrka.api.domain.projects.commands.setProduct;
using cyrka.api.domain.projects.commands.setStatus;
using cyrka.api.web.models;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.services
{
	public class ProjectCommandService
	{
		const string ProjectResourceKey = "projects";

		public ProjectCommandService(CommandProcessor<ProjectAggregate> commandProcessor)
		{
			_commandProcessor = commandProcessor;
		}

		public async Task<IActionResult> Do<TCommand>(TCommand command, string projectId = null)
		{
			var result = await _commandProcessor.ProcessCommand(command, projectId);

			if (result.Exception != null)
				return GetError(result.Exception, projectId);

			if (result.Events == null || result.Events.Length == 0)
				return new OkResult();

			var resourceId = projectId ??
				result.Events
					.Select(e => e.EventData)
					.FirstOrDefault(ped => ped != null)?
					.AggregateId;

			return new OkObjectResult(
				new ApiAnswer
				{
					ResourceType = ProjectResourceKey,
					ResourceId = resourceId
				}
			);
		}

		private IActionResult GetError(CodedException exception, string projectId)
		{
			var apiAnswer = new ApiAnswer
			{
				ResourceType = ProjectResourceKey,
				ResourceId = projectId,
				Error = new Error
				{
					Code = exception.ErrorCode,
					Description = exception.ErrorMessage
				}
			};

			switch (exception)
			{
				case var codedException when (codedException.ErrorCode == GeneralErrors.NotFoundCode):
					return new NotFoundObjectResult(apiAnswer);
			}

			return new BadRequestObjectResult(apiAnswer);
		}
		
		private readonly CommandProcessor<ProjectAggregate> _commandProcessor;
	}

}
