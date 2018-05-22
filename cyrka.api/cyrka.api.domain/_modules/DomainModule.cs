using Autofac;
using cyrka.api.common.commands;
using cyrka.api.common.queries;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.queries;
using cyrka.api.domain.jobs;
using cyrka.api.domain.jobs.queries;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.queries;

namespace cyrka.api.domain._modules
{
	public class DomainModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<CustomerPlainRegistrator>()
				.SingleInstance();
			builder.RegisterType<JobTypePlainRegistrator>()
				.SingleInstance();
			builder.RegisterType<ProjectPlainRegistrator>()
				.SingleInstance();

			builder.RegisterType<CustomerAggregateRepository>();
			builder.RegisterType<JobTypeAggregateRepository>();

			builder.RegisterType<ProjectAggregateRepository>()
				.As<IAggregateRepository<ProjectAggregate>>();

			builder.RegisterBuildCallback(c =>
			{
				var processor = c.Resolve<QueryEventProcessor>();
				c.Resolve<CustomerPlainRegistrator>()
					.RegisterIn(processor);
				c.Resolve<JobTypePlainRegistrator>()
					.RegisterIn(processor);
				c.Resolve<ProjectPlainRegistrator>()
					.RegisterIn(processor);
			});
		}
	}
}
