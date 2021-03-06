using Autofac;
using cyrka.api.common._modules;
using cyrka.api.domain._modules;
using cyrka.api.infra._modules;
using cyrka.api.web._modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace cyrka.api.web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services
				.AddMvc()
				.AddJsonOptions(opts =>
				{
					opts.SerializerSettings.Converters.Add(new StringEnumConverter());
				});

			services.AddAuthentication(opts =>
			{
				opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opts =>
			{
				opts.Authority = Configuration["Auth:Authority"];
				opts.Audience = Configuration["Auth:Audience"];
			});
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterModule(new CommonModule());
			builder.RegisterModule(new DomainModule());

			var writeDbConfig = Configuration.GetSection("MongoDbs:Write").Get<MongoDbConfiguration>();
			var readDbConfig = Configuration.GetSection("MongoDbs:Read").Get<MongoDbConfiguration>();
			builder.RegisterModule(new InfraModule(writeDbConfig, readDbConfig));

			builder.RegisterModule(new WebModule());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseCors(policyBuilder =>
				policyBuilder
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin()
			);
			app.UseMvc();
		}
	}
}
