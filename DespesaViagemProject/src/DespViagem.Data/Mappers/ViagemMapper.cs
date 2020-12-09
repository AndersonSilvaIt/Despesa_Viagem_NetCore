using DespViagem.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespViagem.Data.Mappers
{
	public class ViagemMapper : IEntityTypeConfiguration<Viagem>
	{

		public void Configure(EntityTypeBuilder<Viagem> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(x => x.Descricao)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(x => x.Cliente)
				.HasColumnType("varchar(100)");

			builder.HasOne(s => s.Endereco)
				.WithOne(endereco => endereco.Viagem)
				.HasForeignKey<Endereco>(endereco => endereco.ViagemId);

			builder .HasMany(c => c.Despesas)
				.WithOne(i => i.Viagem)
				.HasForeignKey(c => c.ViagemId);

			builder.ToTable("Viagem");
		}
	}
}
