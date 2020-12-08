using DespViagem.UI.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DespViagem.UI.ViewModels
{
	public class UsuarioRegistro
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[DisplayName("Nome Completo")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[DisplayName("CPF")]
		[Cpf]
		public string CPF { get; set; }

		[DisplayName("E-mail")]
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1] caracteres", MinimumLength = 6)]
		public string Senha { get; set; }


		[Compare("Senha", ErrorMessage = "As senhas não conferem.")]
		[DisplayName("Senha de Confirmação")]
		public string SenhaConfirmacao { get; set; }
	}
	public class UsuarioLogin
	{
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1] caracteres", MinimumLength = 0)]
		public string Senha { get; set; }
	}

	public class UsuarioRespostaLogin
	{
		public string AccessToken { get; set; }
		public double ExpiresIn { get; set; }
		public UsuarioToken UsuarioToken { get; set; }
		//public ResponseResult ResponseResult { get; set; }
	}

	public class UsuarioToken
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public IEnumerable<UsuarioClaim> Claims { get; set; }
	}

	public class UsuarioClaim
	{
		public string Value { get; set; }
		public string Type { get; set; }
	}
}
