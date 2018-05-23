using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.errors;
using cyrka.api.domain.jobs;
using cyrka.api.web.models;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.services
{
	public class JobTypeCommandService
	{
		const string JobTypeResourceKey = "jobtypes";

		public JobTypeCommandService(CommandProcessor<JobTypeAggregate> commandProcessor)
		{
			_commandProcessor = commandProcessor;
		}

		public async Task<IActionResult> Do<TCommand>(TCommand command, string jobTypeId = null)
		{
			var result = await _commandProcessor.ProcessCommand(command, jobTypeId);

			if (result.Exception != null)
				return GetError(result.Exception, jobTypeId);

			if (result.Events == null || result.Events.Length == 0)
				return new OkResult();

			var resourceId = jobTypeId ??
				result.Events
					.Select(e => e.EventData)
					.FirstOrDefault(ped => ped != null)?
					.AggregateId;

			return new OkObjectResult(
				new ApiAnswer
				{
					ResourceType = JobTypeResourceKey,
					ResourceId = resourceId
				}
			);
		}

		private IActionResult GetError(CodedException exception, string jobTypeId)
		{
			var apiAnswer = new ApiAnswer
			{
				ResourceType = JobTypeResourceKey,
				ResourceId = jobTypeId,
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

		private readonly CommandProcessor<JobTypeAggregate> _commandProcessor;
	}
}
