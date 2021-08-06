using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DespViagem.Data.Repository
{
	public class ViagemRepository : BaseRepository<Viagem>, IViagemRepository
	{
		public ViagemRepository(ViagemContext contexto) : base(contexto)
		{
		}

		public async Task<Viagem> ObterViagemEndereco(int id)
		{
			// Obter a viagem junto com seu endereco
			return await Db.Viagens.AsNoTracking()
						.Include(c => c.Endereco)
						.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Viagem> ObterViagemEnderecoDespesa(int id)
		{
			return await Db.Viagens.AsNoTracking()
			.Include(c => c.Despesas)
			.Include(c => c.Endereco)
			.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
