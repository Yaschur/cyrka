using System.Collections.Generic;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.projections.eventProjections;
using cyrka.api.domain.customers.projections.viewModels;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers.projections.eventProjections
{
	[TestFixture]
	public class TitleChangedProjectionShould
	{
		// TODO: Do not change others, Do not change if missing or empty

		[Test]
		public void ChangeAppropriateTitleInTargetView()
		{
			var targetView = new CustomerFullView
			{
				Id = TestContext.CurrentContext.Random.GetString(),
				Name = TestContext.CurrentContext.Random.GetString(),
				Description = TestContext.CurrentContext.Random.GetString(),
				TitlesCount = 3,
				Titles = new List<TitleView>
				{
					new TitleView
					{
						Id = TestContext.CurrentContext.Random.GetString(),
						Name = TestContext.CurrentContext.Random.GetString(),
						Description = TestContext.CurrentContext.Random.GetString(),
						NumberOfSeries = TestContext.CurrentContext.Random.Next(1),
					},
					new TitleView
					{
						Id = TestContext.CurrentContext.Random.GetString(),
						Name = TestContext.CurrentContext.Random.GetString(),
						Description = TestContext.CurrentContext.Random.GetString(),
						NumberOfSeries = TestContext.CurrentContext.Random.Next(1),
					},
					new TitleView
					{
						Id = TestContext.CurrentContext.Random.GetString(),
						Name = TestContext.CurrentContext.Random.GetString(),
						Description = TestContext.CurrentContext.Random.GetString(),
						NumberOfSeries = TestContext.CurrentContext.Random.Next(1),
					},
				}
			};
			var eventData = new TitleChanged(
				targetView.Id,
				targetView.Titles[1].Id,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.Next(1),
				TestContext.CurrentContext.Random.GetString()
			);
			var eventProjectionUnderTest = new TitleChangedProjection();

			var can = eventProjectionUnderTest.CanProject(eventData);
			var res = eventProjectionUnderTest.Project(eventData, targetView);

			Assert.IsTrue(can);
			Assert.AreEqual(eventData.Name, targetView.Titles[1].Name);
			Assert.AreEqual(eventData.NumberOfSeries, targetView.Titles[1].NumberOfSeries);
			Assert.AreEqual(eventData.Description, targetView.Titles[1].Description);
		}
	}
}
