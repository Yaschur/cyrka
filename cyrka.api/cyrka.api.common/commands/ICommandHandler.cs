using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.commands
{
	public interface ICommandHandler
	{
		Task<Event[]> Handle(object command, object aggregate);

		Task<HandledCommandResult> HandleSafely(object command, object aggregate);
	}
}
