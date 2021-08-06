using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Business.Validations;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Services
{
	public class DespesaService : BaseService, IDespesaService
	{
		private readonly IDespesaRepository _despesaRepository;

		public DespesaService(IDespesaRepository despesaRepository,
								INotificador notificador, IUnitOfWork uow) : base(notificador, uow)
		{
			_despesaRepository = despesaRepository;
		}

		public async Task Adicionar(Despesa despesa)
		{
			if (!ExecutarValidacao(new DespesaValidation(), despesa)) return;

			await _despesaRepository.Adicionar(despesa);
		}

		public async Task Atualizar(Despesa despesa)
		{
			if (!ExecutarValidacao(new DespesaValidation(), despesa)) return;

			await _despesaRepository.Atualizar(despesa);
		}

		public async Task Remover(int id)
		{
			await _despesaRepository.Remover(id);
		}

    }
}
