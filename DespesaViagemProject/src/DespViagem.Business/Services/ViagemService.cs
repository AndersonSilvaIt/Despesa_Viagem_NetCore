using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Business.Validations;
using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Services
{
	public class ViagemService : BaseService, IViagemService
	{
		private readonly IViagemRepository _viagemRepository;
		private readonly IEnderecoRepository _enderecoRepository;

		public ViagemService(IViagemRepository fornecedorRepository,
								 IEnderecoRepository enderecoRepository,
								INotificador notificador, IUnitOfWork uow) : base(notificador, uow)
		{
			_viagemRepository = fornecedorRepository;
			_enderecoRepository = enderecoRepository;
		}

		public async Task Adicionar(Viagem viagem)
		{
			if (!ExecutarValidacao(new ViagemValidation(), viagem)
				|| !ExecutarValidacao(new EnderecoValidation(), viagem.Endereco)) return;

			await _viagemRepository.Adicionar(viagem);
		}

		public async Task Atualizar(Viagem viagem)
		{
			if (!ExecutarValidacao(new ViagemValidation(), viagem)) return;

			await _viagemRepository.Atualizar(viagem);
		}

		public async Task AtualizarEndereco(Endereco endereco)
		{
			if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

			await _enderecoRepository.Atualizar(endereco);
		}

		public async Task Remover(int id)
		{
			var endereco = await _enderecoRepository.ObterEnderecoPorViagem(id);

			if (endereco != null)
				await _enderecoRepository.Remover(endereco.Id);

			await _viagemRepository.Remover(id);
		}

		public void PreAdicionar(Despesa despesa)
		{
			if (!ExecutarValidacao(new DespesaValidation(), despesa)) return;
		}

    }
}
