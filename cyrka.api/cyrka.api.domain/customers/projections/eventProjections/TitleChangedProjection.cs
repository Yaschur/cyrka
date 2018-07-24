using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class TitleChangedProjection : BaseEventProjection<TitleChanged, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(TitleChanged eventData, CustomerFullView target)
		{
			var targetTitle = target.Titles.Find(t => t.Id == eventData.TitleId);

			if (targetTitle == null)
				return new EmptyProjectionResult<CustomerFullView>();

			(targetTitle.Name, targetTitle.Description, targetTitle.NumberOfSeries) =
				(eventData.Name, eventData.Description, eventData.NumberOfSeries);

			return new StoreProjectionResult<CustomerFullView>(target);
		}
	}
}
