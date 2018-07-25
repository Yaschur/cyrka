using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjector
	{
		Task Apply(Event eventToApply);
	}
}
