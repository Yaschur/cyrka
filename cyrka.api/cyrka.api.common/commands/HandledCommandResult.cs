using cyrka.api.common.errors;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public class HandledCommandResult
	{
		public readonly CodedException Exception;
		public readonly Event[] EventDatas;

		public HandledCommandResult(Event[] datas)
		{
			EventDatas = datas;
			Exception = null;
		}

		public HandledCommandResult(CodedException exception)
		{
			EventDatas = null;
			Exception = exception;
		}
	}
}
