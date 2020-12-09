using DespViagem.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DespViagem.Data.Mappers
{
	public class EnderecoMapper : IEntityTypeConfiguration<Endereco>
	{
		public void Configure(EntityTypeBuilder<Endereco> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(x => x.Logradouro)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(x => x.Numero)
				.HasColumnType("varchar(20)");

			builder.Property(x => x.Complemento)
				.HasColumnType("varchar(500)");

			builder.Property(x => x.Bairro)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(x => x.Cep)
				.IsRequired()
				.HasColumnType("varchar(10)");

			builder.Property(x => x.Cidade)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(x => x.Estado)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.ToTable("Enderecos");
		}
	}
}
