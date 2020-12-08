using DespViagem.UI.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DespViagem.UI.Services
{
	public class AutenticacaoService : IAutenticacaoService
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AutenticacaoService(SignInManager<IdentityUser> signInManager,
								   UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<IdentityResult> Login(UsuarioLogin usuarioLogin)
		{
			var user = new IdentityUser
			{
				UserName = usuarioLogin.Email,
				Email = usuarioLogin.Email,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, usuarioLogin.Senha);
			
			if (result.Succeeded)
				await _signInManager.SignInAsync(user, false);

			return result;
		}

		public async Task<IdentityResult> Registro(UsuarioRegistro usuarioRegistro)
		{
			var user = new IdentityUser
			{
				UserName = usuarioRegistro.Email,
				Email = usuarioRegistro.Email,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);
			return result;
		}

	}
}
