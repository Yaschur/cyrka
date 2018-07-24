using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.removeTitle;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class TitleRemovedProjection : BaseEventProjection<TitleRemoved, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(TitleRemoved eventData, CustomerFullView target)
		{
			int remCount = target.Titles.RemoveAll(t => t.Id == eventData.TitleId);

			if (remCount == 0)
				return new EmptyProjectionResult<CustomerFullView>();

			target.TitlesCount -= remCount;

			return new StoreProjectionResult<CustomerFullView>(target);
		}
	}
}
