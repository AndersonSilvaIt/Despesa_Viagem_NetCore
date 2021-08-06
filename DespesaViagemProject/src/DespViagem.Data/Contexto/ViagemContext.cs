using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Business.Models.Gerencial;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DespViagem.Data.Contexto
{
	public class ViagemContext : DbContext, IContext
	{
		public ViagemContext(DbContextOptions options) : base(options)
		{ }
		public DbSet<Pessoa> Pessoas { get; set; }
		public DbSet<Viagem> Viagens { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Despesa> Despesas { get; set; }

		public DbSet<Tela> Tela { get; set; }
		public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
		public DbSet<TelaFuncaoPerfilUsuario> TelaFuncaoPerfilUsuario { get; set; }
		public DbSet<DVUser> DVUser { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*Caso não for mapeado alguma propriedade, aqui é setado como default o tipo dela*/
			foreach (var property in modelBuilder.Model.GetEntityTypes()
											.SelectMany(e => e.GetProperties()
											.Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(200)");
			}

			foreach (var property in modelBuilder.Model.GetEntityTypes()
								.SelectMany(e => e.GetProperties()
								.Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))))
			{
				property.SetColumnType("decimal(18,2)");
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ViagemContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}

		async Task<int> IContext.SaveChangesContext()
		{
			int teste = await base.SaveChangesAsync();

			return teste;
		}
	}
}
