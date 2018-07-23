using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IProjectionResult<TView>
		where TView : IView
	{
		Task AccomplishAsync(IProjectionStore<TView> projectionStore);
	}
}
