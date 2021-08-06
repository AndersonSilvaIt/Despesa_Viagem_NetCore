using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;

namespace DespViagem.Data.Repository
{
	public class DespesaRepository : BaseRepository<Despesa>, IDespesaRepository
	{
		public DespesaRepository(ViagemContext contexto) :base(contexto)
		{
		}
	}
}
