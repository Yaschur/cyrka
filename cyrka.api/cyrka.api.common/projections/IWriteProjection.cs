using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IWriteProjection<TView>
		where TView : IView
	{
		Task StoreAsync(TView view);

		Task RemoveAsync(TView view);
	}
}
