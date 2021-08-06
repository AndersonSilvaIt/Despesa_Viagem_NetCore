using DespViagem.Business.Models;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
	public interface IViagemService
	{
		Task Adicionar(Viagem viagem);
		Task Atualizar(Viagem viagem);
		Task Remover(int id);
		Task AtualizarEndereco(Endereco endereco);

		void PreAdicionar(Despesa despesa);
	}
}
