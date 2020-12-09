using DespViagem.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DespViagem.Data.Mappers
{
	public class PessoaMapper : IEntityTypeConfiguration<Pessoa>
	{
		public void Configure(EntityTypeBuilder<Pessoa> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(x => x.Nome)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(x => x.Documento)
				.IsRequired()
				.HasColumnType("varchar(20)");

			builder.Property(x => x.Idade)
				.IsRequired()
				.HasColumnType("integer");

			builder.ToTable("Pessoas");
		}
	}
}
