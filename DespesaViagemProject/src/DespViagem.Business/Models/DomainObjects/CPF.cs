using DespViagem.Business.Validations;

namespace DespViagem.Business.Models.DomainObjects
{
	public class CPF
	{
		public const int CpfMaxLength = 11;
		public string Numero { get; private set; }

		// Construtor do EntityFramework
		protected CPF() { }

		public CPF(string numero)
		{
			//ex
			if (!Validar(numero))
				throw new DomainException("CPF inválido");

			Numero = numero;
		}

		public static bool Validar(string cpf)
		{
			return CpfValidacao.Validar(cpf);
		}

	}
}
