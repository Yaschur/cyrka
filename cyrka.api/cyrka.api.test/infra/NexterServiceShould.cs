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
			var res = await srvUnderTest.GetNextInt(10);

			Assert.AreEqual(11, res);
		}

		[Test]
		public async Task GenerateNextSequenceNumberInParallel()
		{
			var res = await Task.WhenAll(
				srvUnderTest.GetNextInt(10),
				srvUnderTest.GetNextInt(10),
				srvUnderTest.GetNextInt(10)
			);

			Assert.AreNotEqual(10, res[0]);
			Assert.AreNotEqual(10, res[1]);
			Assert.AreNotEqual(10, res[2]);
			Assert.AreNotEqual(res[0], res[1]);
			Assert.AreNotEqual(res[2], res[1]);
		}

		[Test]
		public async Task GenerateNextSequenceNumberInParallelWithSeedDifferent()
		{
			var res = await Task.WhenAll(
				srvUnderTest.GetNextInt(13),
				srvUnderTest.GetNextInt(10),
				srvUnderTest.GetNextInt(14),
				srvUnderTest.GetNextInt(14)
			);

			Assert.AreNotEqual(res[0], res[1]);
			Assert.AreNotEqual(res[2], res[1]);
			Assert.AreNotEqual(res[3], res[1]);
		}
	}
}
