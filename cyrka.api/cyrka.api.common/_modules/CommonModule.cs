using Autofac;
using cyrka.api.common.commands;
using cyrka.api.common.generators;
using cyrka.api.common.queries;

namespace cyrka.api.common._modules
{
	public class CommonModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterGeneric(typeof(CommandProcessor<,>));

			builder
				.RegisterType<NexterGenerator>()
				.SingleInstance();

			builder
				.RegisterType<QueryEventProcessor>()
				.SingleInstance();
		}
	}
}
