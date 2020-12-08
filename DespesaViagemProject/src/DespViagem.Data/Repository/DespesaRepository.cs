using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;

namespace DespViagem.Data.Repository
{
	public class DespesaRepository : Repository<Despesa>, IDespesaRepository
	{
		public DespesaRepository(ViagemContext contexto) :base(contexto)
		{
		}
	}
}
