﻿using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DespViagem.Data.Repository
{
	public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
	{
		public EnderecoRepository(ViagemContext contexto) : base(contexto)
		{
		}

		public async Task<Endereco> ObterEnderecoPorViagem(Guid viagemId)
		{
			//Obtem o endereco por fornecedor
			return await Db.Enderecos.AsNoTracking()
					.FirstOrDefaultAsync(f => f.ViagemId == viagemId);
		}
	}
}
