using Microsoft.AspNetCore.Mvc.Razor;

namespace DespViagem.UI.Extensions
{
	public static class RazorHelpers
	{
		public static string FormatarCEP(this RazorPage page, string cep)
		{
			return "";
			//return quantidade > 0 ? $"Apenas {quantidade} em estoque!" : "Produto esgotado!";
		}
	}
}
