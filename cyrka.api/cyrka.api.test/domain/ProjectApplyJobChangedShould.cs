using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.setJob;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class ProjectApplyJobChangedShould
	{
		[Test]
		public void ChangeRateAndAmountOnJobChange()
		{
			var projId = TestContext.CurrentContext.Random.GetString();
			var jobTypeId = TestContext.CurrentContext.Random.GetString();
			var jobTypeName = TestContext.CurrentContext.Random.GetString();
			var unitName = TestContext.CurrentContext.Random.GetString();
			var ratePerUnit = TestContext.CurrentContext.Random.NextDecimal();
			var amount = TestContext.CurrentContext.Random.NextUInt();
			var subjUnderTest = new ProjectAggregate(new ProjectState
			{
				// ProjectId = projId,
				Jobs = new[]
				{
					new JobState
					{
						JobTypeId = jobTypeId,
						JobTypeName = jobTypeName,
						UnitName = unitName,
						RatePerUnit = ratePerUnit,
						Amount = amount
					}
				}
			});
			var jobSet = new JobChanged(
				projId,
				jobTypeId,
				TestContext.CurrentContext.Random.NextDecimal(),
				TestContext.CurrentContext.Random.NextUInt()
			);

			subjUnderTest.ApplyEvents(new ProjectEventData[] { jobSet });

			Assert.AreEqual(1, subjUnderTest.State.Jobs.Length);
			Assert.AreEqual(jobTypeId, subjUnderTest.State.Jobs[0].JobTypeId);
			Assert.AreEqual(jobSet.Amount, subjUnderTest.State.Jobs[0].Amount);
			Assert.AreEqual(jobTypeName, subjUnderTest.State.Jobs[0].JobTypeName);
			Assert.AreEqual(jobSet.RatePerUnit, subjUnderTest.State.Jobs[0].RatePerUnit);
			Assert.AreEqual(unitName, subjUnderTest.State.Jobs[0].UnitName);
		}
	}
}
