using Autofac;
using cyrka.api.common.queries;

namespace cyrka.api.common._modules
{
	public class CommonModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<QueryEventProcessor>()
				.SingleInstance();
		}
	}
}
