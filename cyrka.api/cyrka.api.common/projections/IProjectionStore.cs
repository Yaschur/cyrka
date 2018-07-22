using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IProjectionStore<TView>
		where TView : IView
	{
		TView GetById(string id);

		Task StoreAsync(TView view);

		Task RemoveAsync(TView view);
	}
}
