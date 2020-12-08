using DespViagem.Business.Models;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
	public interface IViagemRepository : IRepository<Viagem>
	{
		Task<Viagem> ObterViagemEndereco(Guid id); //Obter o endereco da viagem
		Task<Viagem> ObterViagemEnderecoDespesa(Guid id); //Retorna a viagem e a lista de despesas desta viagem
	}
}
