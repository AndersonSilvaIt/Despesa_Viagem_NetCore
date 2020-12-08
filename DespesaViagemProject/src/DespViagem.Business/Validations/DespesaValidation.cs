using DespViagem.Business.Models;
using FluentValidation;

namespace DespViagem.Business.Validations
{
	public class DespesaValidation : AbstractValidator<Despesa>
	{
		public DespesaValidation()
		{
			RuleFor(d => d.Descricao)
				.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
				.Length(2, 100)
				.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");
		}
	}
}
