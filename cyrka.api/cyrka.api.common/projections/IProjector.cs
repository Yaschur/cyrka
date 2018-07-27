using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjector
	{
		/// <summary>
		/// Apply event to appropriate projections
		/// </summary>
		Task Apply(Event eventToApply);

		/// <summary>
		/// Reset projections to start again (drop data)
		/// </summary>
		Task Reset();
	}
}
