using cyrka.api.domain.projects;
using cyrka.api.domain.projects.events;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class ProjectApplyIncomeChangedShould
	{
		[TestCase(100, true)]
		[TestCase(100, false)]
		[TestCase(55, false)]
		public void ChangeTurnoversByAdditions(decimal val, bool isExp)
		{
			var projectState = new ProjectState();
			if (TestContext.CurrentContext.Random.NextBool())
			{
				projectState.Money = new IncomeStatement
				{
					Income = TestContext.CurrentContext.Random.NextDecimal(1000000),
					Expenses = TestContext.CurrentContext.Random.NextDecimal(1000000)
				};
			}
			var subjUnderTest = new ProjectAggregate(projectState);
			var eventData = new IncomeChanged(TestContext.CurrentContext.Random.GetString(), val, isExp);
			var initIncome = projectState.Money?.Income ?? 0;
			var initExpenses = projectState.Money?.Expenses ?? 0;

			subjUnderTest.ApplyEvents(new[] { eventData });

			Assert.AreEqual(expected: isExp ? initIncome : val, actual: subjUnderTest.State.Money.Income);
			Assert.AreEqual(expected: isExp ? val : initExpenses, actual: subjUnderTest.State.Money.Expenses);
		}
	}
}
