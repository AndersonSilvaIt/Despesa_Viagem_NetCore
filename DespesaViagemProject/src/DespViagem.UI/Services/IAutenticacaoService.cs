using DespViagem.UI.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DespViagem.UI.Services
{
	public interface IAutenticacaoService
	{
		Task<IdentityResult> Login(UsuarioLogin usuarioLogin);
		Task<IdentityResult> Registro(UsuarioRegistro usuarioRegistro);
	}
}
