using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace cyrka.api.web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
					.UseKestrel(options => options.Listen(IPAddress.Any, 5000))
					.UseStartup<Startup>()
					.Build();
	}
}
