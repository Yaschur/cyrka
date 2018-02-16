using Autofac;
using cyrka.api.web.services;

namespace cyrka.api.web._modules
{
	public class WebModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<ProjectService>();
		}
	}
}
