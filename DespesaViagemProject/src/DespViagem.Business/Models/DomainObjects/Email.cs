using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DespViagem.Business.Models.DomainObjects
{
	public class Email
	{
		public const int EnderecoMaxLength = 54;
		public const int EnderecoMinLength = 54;

		public string Endereco { get; private set; }

		protected Email() { }

		public static bool Validar(string email)
		{
			var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
			return regexEmail.IsMatch(email);
		}

	}
}
