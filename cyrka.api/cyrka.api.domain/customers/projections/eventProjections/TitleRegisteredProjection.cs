using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.registerTitle;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class TitleRegisteredProjection : BaseEventProjection<TitleRegistered, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(TitleRegistered eventData, CustomerFullView target)
		{
			target.Titles.Add(new TitleView
			{
				Id = eventData.TitleId,
				Name = eventData.Name,
				Description = eventData.Description,
				NumberOfSeries = eventData.NumberOfSeries,
			});
			target.TitlesCount += 1;

			return new StoreProjectionResult<CustomerFullView>(target);
		}
	}
}
