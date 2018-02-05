using Autofac;
using cyrka.api.common.queries;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.queries;
using cyrka.api.domain.jobs.queries;

namespace cyrka.api.domain._modules
{
	public class DomainModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<CustomerPlainRegistrator>()
				.SingleInstance();

			builder.RegisterType<CustomerAggregateRepository>();

			builder.RegisterBuildCallback(c =>
			{
				var processor = c.Resolve<QueryEventProcessor>();
				c.Resolve<CustomerPlainRegistrator>()
					.RegisterIn(processor);
				c.Resolve<JobTypePlainRegistrator>()
					.RegisterIn(processor);
			});
		}
	}
}
