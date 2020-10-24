using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;

namespace DespViagem.Data.Repository
{
	public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
	{
		public PessoaRepository(ViagemContext contexto) : base(contexto)
		{
		}
	}
}
