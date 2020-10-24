using DespViagem.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DespViagem.Data.Contexto
{
	public class ViagemContext : DbContext
	{
		public ViagemContext(DbContextOptions options) : base(options)
		{ }
		public DbSet<Pessoa> Pessoas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*Caso não for mapeado alguma propriedade, aqui é setado como default o tipo dela*/
			foreach (var property in modelBuilder.Model.GetEntityTypes()
											.SelectMany(e => e.GetProperties()
											.Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(200)");
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ViagemContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}
	}
}
