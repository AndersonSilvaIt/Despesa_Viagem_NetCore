using FluentValidation;

namespace DespViagem.Business.Models.Validations
{
	public class PessoaValidation : AbstractValidator<Pessoa>
	{
		public PessoaValidation()
		{
			RuleFor(p => p.Nome)
				.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
				.Length(2, 100)
				.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");

			//RuleFor(p => p.Profissao)
			//	.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
			//	.Length(2, 100)
			//	.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MatLength} caracteres.");

			RuleFor(p => p.Documento.Length).Equal(ValidacaoCPF.TamanhoCpf)
						.WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

			RuleFor(p => ValidacaoCPF.Validar(p.Documento)).Equal(true)
						.WithMessage("O Documento fornecido é inválido.");

			//RuleFor(p => p.DataNascimento)
			//		.NotEqual(DateTime.MinValue)
			//		.WithMessage("Data de Nascimento inválida");
			//
			//RuleFor(p => p.Idade)
			//		.GreaterThan(0)
			//		.WithMessage("Idade precisa ser maior que zero");
		}
	}
}
