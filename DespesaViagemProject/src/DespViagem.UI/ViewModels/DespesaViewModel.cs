using DespViagem.UI.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DespViagem.UI.ViewModels
{
	public class DespesaViewModel 
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		[DisplayName("Descrição")]
		public string Descricao { get; set; }

		public Guid ViagemId { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Local { get; set; }

		[Moeda]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public decimal Valor { get; set; }

		[DisplayName("Observação")]
		[StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Observacao { get; set; }

		[ScaffoldColumn(false)] // não irá criar o campo na tela quando for fazer o scafolding (criar a view)
		[DisplayName("Data de Cadastro")]
		public DateTime DataCadastro { get; set; }

		public ViagemViewModel Viagem { get; set; }
	
	}
}
