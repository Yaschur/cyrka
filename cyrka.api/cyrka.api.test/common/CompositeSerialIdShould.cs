using System;
using System.Linq;
using cyrka.api.common.identities;
using NUnit.Framework;

namespace cyrka.api.test.common
{
	[TestFixture]
	public class CompositeSerialIdShould
	{
		[Test]
		public void BeCreatedFromPartsToString()
		{
			var rand = TestContext.CurrentContext.Random;
			var serial = rand.NextULong();
			var prefixesCount = rand.NextUShort(0, 10);
			var prefixes = Enumerable.Range(0, prefixesCount)
				.Select(i =>
				{
					object res;
					if (rand.NextBool())
						res = rand.GetString(rand.Next(1, 20));
					else if (rand.NextBool())
						res = rand.NextULong();
					else res = new DateTime(rand.NextLong(DateTime.MinValue.Ticks, DateTime.MinValue.Ticks));
					return res;
				})
				.ToArray();
			var subjToTest = new CompositeSerialId(serial, prefixes);

			var actualId = subjToTest.ToString();

			for (int i = 0; i < prefixes.Length - 1; i++)
				Assert.IsTrue(actualId.Contains(prefixes[i].ToString()));
			Assert.AreEqual(serial, subjToTest.Serial);
		}

		[Test]
		public void BeGoodOnEmptyPrefixes()
		{
			var rand = TestContext.CurrentContext.Random;
			var serial = rand.NextULong();
			var subjToTest = new CompositeSerialId(serial);

			var actualId = subjToTest.ToString();
			Assert.AreEqual(serial, ulong.Parse(actualId));
		}

		[Test]
		public void BeCastToStringImplicitly()
		{
			var subjUnderTest = new CompositeSerialId(298, "SUBST", "18", "02");
			var expectedId = "SUBST-18-02-298";

			var actualId = (string)subjUnderTest;

			Assert.AreEqual(expectedId, actualId);
		}

		[Test]
		public void BeCastFromStringExplicitly()
		{
			var expectedId = "SUBST-1802-298";
			var subjUnderTest = (CompositeSerialId)expectedId;

			var actualId = (string)subjUnderTest;

			Assert.AreEqual(expectedId, actualId);
		}

		[Test]
		public void ThrowIfCastFromStringWithoutNumberAtEnd()
		{
			var stringId = "i-throw-exception";
			Assert.Throws<InvalidCastException>(() => { var x = (CompositeSerialId)stringId; });
		}

		[Test]
		public void GetSerialNumberOfId()
		{
			var rand = TestContext.CurrentContext.Random;
			var expectedSerial = rand.NextULong();
			var prefixesCount = rand.NextUShort(0, 10);
			var prefixes = Enumerable.Range(0, prefixesCount)
				.Select(i =>
				{
					object res;
					if (rand.NextBool())
						res = rand.GetString(rand.Next(1, 20));
					else if (rand.NextBool())
						res = rand.NextULong();
					else res = new DateTime(rand.NextLong(DateTime.MinValue.Ticks, DateTime.MinValue.Ticks));
					return res;
				})
				.ToArray();
			var subjToTest = new CompositeSerialId(expectedSerial, prefixes);

			var actualSerial = subjToTest.Serial;

			Assert.AreEqual(expectedSerial, actualSerial);
		}
	}
}
