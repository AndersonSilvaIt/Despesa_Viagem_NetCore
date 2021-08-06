using DespViagem.Business.Models;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
    public interface IViagemRepository : IRepository<Viagem>
	{
		Task<Viagem> ObterViagemEndereco(int id); //Obter o endereco da viagem
		Task<Viagem> ObterViagemEnderecoDespesa(int id); //Retorna a viagem e a lista de despesas desta viagem
	}
}
