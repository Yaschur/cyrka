namespace cyrka.api.web.services
{
	public class JobTypeCommandService
	{
		public JobTypeCommandService(CommandProcessor<JobTypeAggregate> commandProcessor)
		{
			_commandProcessor = commandProcessor;
		}

		private readonly CommandProcessor<JobTypeAggregate> _commandProcessor;
	}
