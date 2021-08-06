using DespViagem.Business.Interfaces;
using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Interfaces.services;
using DespViagem.Business.Notificacoes;
using DespViagem.Business.Services;
using DespViagem.Business.Services.gerencial;
using DespViagem.Data.Repository;
using DespViagem.Data.Repository.Gerencial;
using DespViagem.Data.UoW;
using DespViagem.UI.Extensions;
using DespViagem.UI.Services;
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

			services.AddScoped<IViagemRepository, ViagemRepository>();
			services.AddScoped<IPessoaRepository, PessoaRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();

			services.AddScoped<IPessoaService, PessoaService>();
			services.AddScoped<IViagemService, ViagemService>();

			services.AddScoped<IDVUserRepository, DVUserRepository>();
			services.AddScoped<IDVUserService, DVUserService>();

			services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
			services.AddScoped<IPerfilUsuarioService, PerfilUsuarioService>();
			
			services.AddScoped<IIdentityService, IdentityService>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
