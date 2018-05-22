using cyrka.api.common.errors;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public class ProcessedCommandResult
	{
		public readonly CodedException Exception;
		public readonly Event[] Events;

		public ProcessedCommandResult(Event[] datas)
		{
			Events = datas;
			Exception = null;
		}

		public ProcessedCommandResult(CodedException exception)
		{
			Events = null;
			Exception = exception;
		}
	}
}
