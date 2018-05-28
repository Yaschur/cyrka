using cyrka.api.domain.projects;
using cyrka.api.domain.projects.events;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class ProjectApplyIncomeChangedShould
	{
		[TestCase(0, 100)]
		[TestCase(100, 0)]
		[TestCase(-44, 55)]
		public void ChangeTurnoversByAdditions(decimal inc, decimal exp)
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
			var eventData = new IncomeChanged(
				TestContext.CurrentContext.Random.GetString(),
				inc,
				exp
			);
			var initIncome = projectState.Money?.Income ?? 0;
			var initExpenses = projectState.Money?.Expenses ?? 0;

			subjUnderTest.ApplyEvents(new[] { eventData });

			Assert.AreEqual(expected: initIncome + eventData.IncomeAddition, actual: subjUnderTest.State.Money.Income);
			Assert.AreEqual(expected: initExpenses + eventData.ExpensesAddition, actual: subjUnderTest.State.Money.Expenses);
		}
	}
}
