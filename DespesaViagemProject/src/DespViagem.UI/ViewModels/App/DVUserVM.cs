using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DespViagem.UI.ViewModels.App
{
    public class DVUserVM 
    {
        public int Id { get; set; }
        //[DisplayNamePW(nameof(Name)), RequiredPW(nameof(Name))]
        public string Name { get; set; }

		//[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[DisplayName("CPF")]
		////[Cpf]
		//public string CPF { get; set; }

		//[DisplayName("E-mail")]
		//[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		//public string Email { get; set; }

		//[DisplayNamePW(nameof(UserName)), RequiredPW(nameof(UserName))]
		//[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string UserName { get; set; }

		[NotMapped]
		//[DisplayNamePW(nameof(Password)), RequiredPW(nameof(Password))]
		//[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1] caracteres", MinimumLength = 6)]
		public string Password { get; set; }

		[NotMapped]
		public string UserNameHidden { get; set; }

		[NotMapped]
		public string PasswordHidden { get; set; }

		[NotMapped]
		[Compare("Senha", ErrorMessage = "As senhas não conferem.")]
		//[DisplayName("Senha de Confirmação")]
		public string ConfirmPassword { get; set; }

		[NotMapped]
		//[DisplayNamePW(nameof(ChangePassword))]
		public bool ChangePassword { get; set; }

		public string IdentityRerefenceId { get; set; }

		//[DisplayNamePW("UserPerfil"), RequiredPW("UserPerfil")]
		public int UserPerfilId { get; set; }

		//[DisplayNamePW("UserPerfil")]
		public IEnumerable<PerfilUsuarioVM> PerfilUsuario { get; set; }

	}

	public class UserLogin
	{
		//[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Email { get; set; }

		//[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
		public string Login { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		//[StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1] caracteres", MinimumLength = 0)]
		public string PwdLogin { get; set; }
	}

	public class UserResponseLogin
	{
		public string AccessToken { get; set; }
		public double ExpiresIn { get; set; }
		public UserToken UserToken { get; set; }
		//public ResponseResult ResponseResult { get; set; }
	}

	public class UserToken
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public IEnumerable<UserClaim> Claims { get; set; }
	}

	public class UserClaim
	{
		public string Value { get; set; }
		public string Type { get; set; }
	}

}
