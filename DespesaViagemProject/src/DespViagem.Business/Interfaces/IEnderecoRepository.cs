using DespViagem.Business.Models;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
	public interface IEnderecoRepository : IRepository<Endereco>
	{
		Task<Endereco> ObterEnderecoPorViagem(Guid viagemId); // Obtem um endereço através da viagem
	}
}
