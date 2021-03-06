using System;
using Autofac;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.common.generators;
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

			builder.RegisterType<JobTypeAggregateRepository>()
				.As<IAggregateRepository<JobTypeAggregate>>();
			builder.RegisterType<ProjectAggregateRepository>()
				.As<IAggregateRepository<ProjectAggregate>>();
			builder.RegisterType<CustomerAggregateRepository>()
				.As<IAggregateRepository<CustomerAggregate>>();

			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(IAggregateCommandHandler<,>));

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
