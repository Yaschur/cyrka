using cyrka.api.common.errors;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public class ProcessedCommandResult
	{
		public readonly CodedException Exception;
		public readonly Event[] EventDatas;

		public ProcessedCommandResult(Event[] datas)
		{
			EventDatas = datas;
			Exception = null;
		}

		public ProcessedCommandResult(CodedException exception)
		{
			EventDatas = null;
			Exception = exception;
		}
	}
}
