using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.setJob;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class ProjectApplyJobSetShould
	{
		[Test]
		public void HaveNoJobsWithoutJobSet()
		{
			var subjUnderTest = new ProjectAggregate();

			Assert.IsEmpty(subjUnderTest.State.Jobs);
		}

		[Test]
		public void AddJobOnJobSet()
		{
			var subjUnderTest = new ProjectAggregate();
			var jobSet = new JobSet(
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.NextDecimal(),
				TestContext.CurrentContext.Random.NextUInt()
			);

			subjUnderTest.ApplyEvents(new[] { jobSet });

			Assert.IsNotEmpty(subjUnderTest.State.Jobs);
			Assert.AreEqual(1, subjUnderTest.State.Jobs.Length);
		}

		[Test]
		public void ReplaceJobOnJobSetByJobTypeId()
		{
			var projId = TestContext.CurrentContext.Random.GetString();
			var jobTypeId = TestContext.CurrentContext.Random.GetString();
			var jobTypeName = TestContext.CurrentContext.Random.GetString();
			var unitName = TestContext.CurrentContext.Random.GetString();
			var ratePerUnit = TestContext.CurrentContext.Random.NextDecimal();
			var amount = TestContext.CurrentContext.Random.NextUInt();
			var state = new ProjectState()
			{
				// ProjectId = projId,
				Jobs = new[]
				{
					new JobState
					{
						Amount = amount,
						JobTypeId = jobTypeId,
						JobTypeName = jobTypeName,
						RatePerUnit = ratePerUnit,
						UnitName = unitName
					}
				}
			};
			var subjUnderTest = new ProjectAggregate(state);
			var jobSet = new JobSet(
				projId,
				jobTypeId,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.NextDecimal(),
				TestContext.CurrentContext.Random.NextUInt()
			);

			subjUnderTest.ApplyEvents(new[] { jobSet });

			Assert.AreEqual(1, subjUnderTest.State.Jobs.Length);
			Assert.AreEqual(jobTypeId, subjUnderTest.State.Jobs[0].JobTypeId);
			Assert.AreEqual(jobSet.Amount, subjUnderTest.State.Jobs[0].Amount);
			Assert.AreEqual(jobSet.JobTypeName, subjUnderTest.State.Jobs[0].JobTypeName);
			Assert.AreEqual(jobSet.RatePerUnit, subjUnderTest.State.Jobs[0].RatePerUnit);
			Assert.AreEqual(jobSet.UnitName, subjUnderTest.State.Jobs[0].UnitName);
		}
	}
}
