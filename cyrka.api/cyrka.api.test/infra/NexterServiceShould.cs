using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.infra.nexter;
using NUnit.Framework;

namespace cyrka.api.test.infra
{
	[TestFixture]
	class NexterServiceShould
	{
		NexterGenerator srvUnderTest;

		[SetUp]
		public void Setup()
		{
			srvUnderTest = new NexterGenerator();
		}

		[Test]
		public async Task GenerateNextSequenceNumber()
		{
			var res = await srvUnderTest.GetNextNumber("test", 10);

			Assert.AreEqual(11, res);
		}

		[Test]
		public async Task GenerateNextSequenceNumbersInParallel()
		{
			var key = "test";

			var res = await Task.WhenAll(
				srvUnderTest.GetNextNumber(key, 10),
				srvUnderTest.GetNextNumber(key, 10),
				srvUnderTest.GetNextNumber(key, 10)
			);

			Assert.AreNotEqual(10, res[0]);
			Assert.AreNotEqual(10, res[1]);
			Assert.AreNotEqual(10, res[2]);
			Assert.AreNotEqual(res[0], res[1]);
			Assert.AreNotEqual(res[2], res[1]);
		}

		[Test]
		public async Task GenerateNextSequenceNumbersInParallelWithSeedDifferent()
		{
			var key = "test";

			var res = await Task.WhenAll(
				srvUnderTest.GetNextNumber(key, 13),
				srvUnderTest.GetNextNumber(key, 10),
				srvUnderTest.GetNextNumber(key, 14),
				srvUnderTest.GetNextNumber(key, 14)
			);

			Assert.AreNotEqual(res[0], res[1]);
			Assert.AreNotEqual(res[2], res[1]);
			Assert.AreNotEqual(res[3], res[1]);
		}

		[Test]
		public async Task GenerateNextSequenceNumbersInParallelWithKeysDifferent()
		{
			var key1 = "test";
			var key2 = "probe";
			var ts1 = Enumerable
				.Range(0, TestContext.CurrentContext.Random.Next(1000))
				.Select(i => srvUnderTest.GetNextNumber(key1, 10))
				.ToList();
			var ts2 = Enumerable
				.Range(0, TestContext.CurrentContext.Random.Next(1000))
				.Select(i => srvUnderTest.GetNextNumber(key2, 10))
				.ToList();

			await Task.WhenAll(ts1.Union(ts2));
			var res1 = ts1.Select(t => t.Result).ToList();
			var res2 = ts2.Select(t => t.Result).ToList();

			Assert.AreEqual(res1.Count, res1.Distinct().Count());
			Assert.AreEqual(res2.Count, res2.Distinct().Count());
		}
	}
}
