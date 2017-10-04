using System;
using System.Threading.Tasks;
using cyrka.api.infra.nexter;
using NUnit.Framework;

namespace cyrka.api.test.infra
{
	[TestFixture]
	class NexterServiceShould
	{
		NexterService srvUnderTest;

		[SetUp]
		public void Setup()
		{
			srvUnderTest = new NexterService();
		}

		[Test]
		public async Task GenerateNextSequenceNumber()
		{
			var res = await srvUnderTest.GetNextInt("test", 10);

			Assert.AreEqual(11, res);
		}

		[Test]
		public async Task GenerateNextSequenceNumbersInParallel()
		{
			var key = "test";

			var res = await Task.WhenAll(
				srvUnderTest.GetNextInt(key, 10),
				srvUnderTest.GetNextInt(key, 10),
				srvUnderTest.GetNextInt(key, 10)
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
				srvUnderTest.GetNextInt(key, 13),
				srvUnderTest.GetNextInt(key, 10),
				srvUnderTest.GetNextInt(key, 14),
				srvUnderTest.GetNextInt(key, 14)
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

			var t1 = srvUnderTest.GetNextInt(key1, 10);
			var t2 = srvUnderTest.GetNextInt(key1, 10);
			var t3 = srvUnderTest.GetNextInt(key2, 10);
			var t4 = srvUnderTest.GetNextInt(key1, 10);
			var t5 = srvUnderTest.GetNextInt(key2, 10);
			await Task.WhenAll(t1, t2, t3, t4, t5);

			Assert.AreNotEqual(t1.Result, t2.Result);
			Assert.AreNotEqual(t1.Result, t4.Result);
			Assert.AreNotEqual(t3.Result, t5.Result);
			Assert.AreEqual(t1.Result, t3.Result);
		}
	}
}
