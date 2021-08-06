using DespViagem.Business.Models;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
	{
		Task<Endereco> ObterEnderecoPorViagem(int viagemId); // Obtem um endereço através da viagem
	}
}
