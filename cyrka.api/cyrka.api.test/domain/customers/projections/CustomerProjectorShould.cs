using System.Linq;
using cyrka.api.common.projections;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.projections;
using cyrka.api.domain.customers.projections.viewModels;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers.projections
{
	[TestFixture]
	public class CustomerProjectorShould
	{
		CustomerProjector _projectorUnderTest;

		private IWriteProjection<CustomerFullView> _projectionWriter;
		private IReadProjection<CustomerFullView> _projectionReader;
		private IProjectionOfEvent<CustomerRegistered, CustomerFullView> _projectionOfCustomerCustomerRegistered;

		[SetUp]
		public void Setup()
		{

			_projectorUnderTest = new CustomerProjector(
				_projectionReader,
				_projectionWriter,
				new[] {
					_projectionOfCustomerCustomerRegistered
				}
			);
		}

		[Test]
		public void ProcessCustomerRegistered()
		{

		}
	}
}
