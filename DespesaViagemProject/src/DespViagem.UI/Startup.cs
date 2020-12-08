using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DespViagem.UI.Configuration;
using AutoMapper;

namespace DespViagem.UI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityConfiguration(Configuration);
			services.AddMvcConfiguration(Configuration);

			services.AddAutoMapper(typeof(Startup));

			services.RegisterServices(Configuration);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMvcConfiguration(env);
		}
	}
}
