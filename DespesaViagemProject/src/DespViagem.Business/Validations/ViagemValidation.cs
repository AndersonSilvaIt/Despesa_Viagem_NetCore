using DespViagem.Business.Models;
using FluentValidation;

namespace DespViagem.Business.Validations
{
	public class ViagemValidation : AbstractValidator<Viagem>
	{
		public ViagemValidation()
		{
			RuleFor(f => f.Descricao)
				.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
				.Length(2, 100)
				.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");

			RuleFor(f => f.Cliente)
				.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
				.Length(2, 100)
				.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");
		}
	}
}
