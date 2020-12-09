using DespViagem.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespViagem.Data.Mappers
{
	public class DespesaMapper : IEntityTypeConfiguration<Despesa>
	{
		public void Configure(EntityTypeBuilder<Despesa> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(x => x.Descricao)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(x => x.Local)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(x => x.Observacao)
				.HasColumnType("varchar(1000)");

			builder.ToTable("Despesa");
		}
	}
}
