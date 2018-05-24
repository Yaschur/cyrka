using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.errors;
using cyrka.api.domain.customers;
using cyrka.api.domain.jobs;
using cyrka.api.web.models;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.services
{
	public class CustomerCommandService
	{
		const string CustomerResourceKey = "customers";

		public CustomerCommandService(CommandProcessor<CustomerAggregate> commandProcessor)
		{
			_commandProcessor = commandProcessor;
		}

		public async Task<IActionResult> Do<TCommand>(TCommand command, string customerId = null)
		{
			var result = await _commandProcessor.ProcessCommand(command, customerId);

			if (result.Exception != null)
				return GetError(result.Exception, customerId);

			if (result.Events == null || result.Events.Length == 0)
				return new OkResult();

			var resourceId = customerId ??
				result.Events
					.Select(e => e.EventData)
					.FirstOrDefault(ped => ped != null)?
					.AggregateId;

			return new OkObjectResult(
				new ApiAnswer
				{
					ResourceType = CustomerResourceKey,
					ResourceId = resourceId
				}
			);
		}

		private IActionResult GetError(CodedException exception, string jobTypeId)
		{
			var apiAnswer = new ApiAnswer
			{
				ResourceType = CustomerResourceKey,
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

		private readonly CommandProcessor<CustomerAggregate> _commandProcessor;
	}
}
