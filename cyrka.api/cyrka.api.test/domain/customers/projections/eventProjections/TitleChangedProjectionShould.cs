using System.Collections.Generic;
using System.Linq;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.projections.eventProjections;
using cyrka.api.domain.customers.projections.viewModels;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers.projections.eventProjections
{
	[TestFixture]
	public class TitleChangedProjectionShould
	{
		[Test]
		public void ChangeAppropriateTitleInTargetView()
		{
			var targetView = GenerateCustomerFullView(TestContext.CurrentContext.Random.Next(1, 9999));
			var targetIndex = TestContext.CurrentContext.Random.Next(1, targetView.TitlesCount);
			var targetTitle = targetView.Titles[targetIndex];
			var eventData = new TitleChanged(
				targetView.Id,
				targetTitle.Id,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.Next(1),
				TestContext.CurrentContext.Random.GetString()
			);
			var eventProjectionUnderTest = new TitleChangedProjection();

			var can = eventProjectionUnderTest.CanProject(eventData);
			eventProjectionUnderTest.Project(eventData, targetView);

			Assert.IsTrue(can);
			Assert.AreEqual(eventData.Name, targetTitle.Name);
			Assert.AreEqual(eventData.NumberOfSeries, targetTitle.NumberOfSeries);
			Assert.AreEqual(eventData.Description, targetTitle.Description);
		}

		[Test]
		public void ProjectStoreProjectionResult()
		{
			var targetView = GenerateCustomerFullView(TestContext.CurrentContext.Random.Next(1, 9999));
			var targetIndex = TestContext.CurrentContext.Random.Next(1, targetView.TitlesCount);
			var targetTitle = targetView.Titles[targetIndex];
			var eventData = new TitleChanged(
				targetView.Id,
				targetTitle.Id,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.Next(1),
				TestContext.CurrentContext.Random.GetString()
			);
			var eventProjectionUnderTest = new TitleChangedProjection();

			var res = eventProjectionUnderTest.Project(eventData, targetView);

			Assert.IsInstanceOf<StoreProjectionResult<CustomerFullView>>(res);
		}

		[Test]
		public void NotChangeOtherTitlesInTargetView()
		{
			var targetView = GenerateCustomerFullView(TestContext.CurrentContext.Random.Next(2, 9999));
			var targetIndex = TestContext.CurrentContext.Random.Next(1, targetView.TitlesCount);
			var targetTitleId = targetView.Titles[targetIndex].Id;
			var eventData = new TitleChanged(
				targetView.Id,
				targetTitleId,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.Next(1),
				TestContext.CurrentContext.Random.GetString()
			);
			var eventProjectionUnderTest = new TitleChangedProjection();

			eventProjectionUnderTest.Project(eventData, targetView);

			foreach (var ot in targetView.Titles.Where(t => t.Id != targetTitleId))
			{
				Assert.AreNotEqual(eventData.Name, ot.Name);
				Assert.AreNotEqual(eventData.NumberOfSeries, ot.NumberOfSeries);
				Assert.AreNotEqual(eventData.Description, ot.Description);
			}
		}

		[Test]
		public void NotChangeTitlesIfNoAppropriateById()
		{
			var targetView = GenerateCustomerFullView(TestContext.CurrentContext.Random.Next(0, 9999));
			var eventData = new TitleChanged(
				targetView.Id,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.Next(1),
				TestContext.CurrentContext.Random.GetString()
			);
			var eventProjectionUnderTest = new TitleChangedProjection();

			eventProjectionUnderTest.Project(eventData, targetView);

			foreach (var ot in targetView.Titles)
			{
				Assert.AreNotEqual(eventData.Name, ot.Name);
				Assert.AreNotEqual(eventData.NumberOfSeries, ot.NumberOfSeries);
				Assert.AreNotEqual(eventData.Description, ot.Description);
			}
		}

		private CustomerFullView GenerateCustomerFullView(int titleCount)
		{
			var resultView = new CustomerFullView
			{
				Id = TestContext.CurrentContext.Random.GetString(),
				Name = TestContext.CurrentContext.Random.GetString(),
				Description = TestContext.CurrentContext.Random.GetString(),
				TitlesCount = titleCount,
				Titles = Enumerable.Range(0, titleCount)
					.Select(_ => new TitleView
					{
						Id = TestContext.CurrentContext.Random.GetString(),
						Name = TestContext.CurrentContext.Random.GetString(),
						Description = TestContext.CurrentContext.Random.GetString(),
						NumberOfSeries = TestContext.CurrentContext.Random.Next(),
					})
					.ToList()
			};

			return resultView;
		}
	}
}
