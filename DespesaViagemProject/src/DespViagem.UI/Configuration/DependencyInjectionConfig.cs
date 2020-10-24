using DespViagem.Business.Interfaces;
using DespViagem.Business.Notificacoes;
using DespViagem.Business.Services;
using DespViagem.Data.Repository;
using DespViagem.UI.Extensions;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DespViagem.UI.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
			services.AddScoped<INotificador, Notificador>();
			services.AddScoped<IPessoaService, PessoaService>();
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			//services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
		}
	}
}
