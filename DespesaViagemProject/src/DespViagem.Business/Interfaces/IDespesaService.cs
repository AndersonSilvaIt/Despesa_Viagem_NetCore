using DespViagem.Business.Models;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
	public interface IDespesaService
	{
		Task Adicionar(Despesa viagem);
		Task Atualizar(Despesa viagem);
		Task Remover(Guid id);
		
	}
}
