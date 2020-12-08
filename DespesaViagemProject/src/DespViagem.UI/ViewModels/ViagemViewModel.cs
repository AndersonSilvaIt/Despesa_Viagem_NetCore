using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespViagem.UI.ViewModels
{
	public class ViagemViewModel
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Descricao { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
		public string Cliente { get; set; }

		[NotMapped]
		public DespesaViewModel Despesa { get; set; }

		public EnderecoViewModel Endereco { get; set; }

		public List<DespesaViewModel> Despesas { get; set; } = new List<DespesaViewModel>();
	}
}
