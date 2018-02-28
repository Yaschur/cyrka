using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.setJob;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class ProjectAggregateApplyEventsShould
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
			var subjUnderTest = new ProjectAggregate();
			var jobSet1 = new JobSet(
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.NextDecimal(),
				TestContext.CurrentContext.Random.NextUInt()
			);
			var jobSet2 = new JobSet(
				jobSet1.AggregateId,
				jobSet1.JobTypeId,
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.GetString(),
				TestContext.CurrentContext.Random.NextDecimal(),
				TestContext.CurrentContext.Random.NextUInt()
			);

			subjUnderTest.ApplyEvents(new[] { jobSet1, jobSet2 });

			Assert.AreEqual(1, subjUnderTest.State.Jobs.Length);
			Assert.AreEqual(jobSet1.JobTypeId, subjUnderTest.State.Jobs[0].JobTypeId);
			Assert.AreEqual(jobSet2.Amount, subjUnderTest.State.Jobs[0].Amount);
			Assert.AreEqual(jobSet2.JobTypeName, subjUnderTest.State.Jobs[0].JobTypeName);
			Assert.AreEqual(jobSet2.RatePerUnit, subjUnderTest.State.Jobs[0].RatePerUnit);
			Assert.AreEqual(jobSet2.UnitName, subjUnderTest.State.Jobs[0].UnitName);
		}
	}
}
