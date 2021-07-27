using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Business.Models.DomainObjects;
using DespViagem.UI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DespViagem.UI.Controllers
{
	public class IdentidadeController : BaseController
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IPessoaService _pessoaService;

		public IdentidadeController(INotificador notificador,
							  SignInManager<IdentityUser> signInManager,
								   UserManager<IdentityUser> userManager,
								   IPessoaService pessoaService) 
			: base(notificador)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_pessoaService = pessoaService;
		}

		[HttpGet]
		[Route("nova-conta")]
		public IActionResult Registro()
		{
			return View();
		}

		[HttpGet]
		[Route("cria-admin")]
		public async Task<IActionResult> CriaAdmin()
		{
			var user = new IdentityUser
			{
				UserName = "admin@gmail.com",
				Email = "admin@gmail.com",
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, "ABCD@1234");

			if (!result.Succeeded)
			{


				return View();
			}
			return View();
		}

		[HttpPost]
		[Route("nova-conta")]
		public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
		{
			if (!ModelState.IsValid) return View(usuarioRegistro);

			if (!Email.Validar(usuarioRegistro.Email))
			{
				NotificarErro("E-mail inválido");

				return View(usuarioRegistro);
			}

			var user = new IdentityUser
			{
				UserName = usuarioRegistro.Email,
				Email = usuarioRegistro.Email,
				EmailConfirmed = true
			};
			
			var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

			if(!result.Succeeded)
			{
				foreach (var error in result.Errors)
					NotificarErro(error.Description);

				return View(usuarioRegistro);
			}

			//Criar Pessoa
			Pessoa pessoa = new Pessoa();
			pessoa.Nome = usuarioRegistro.Nome;
			pessoa.Id = Guid.Parse(user.Id);
			pessoa.Documento = usuarioRegistro.CPF;
			pessoa.Idade = 10;
				
			await _pessoaService.Adicionar(pessoa);

			if(!OperacaoValida())
				return View(usuarioRegistro);


			return RedirectToAction("Identidade", "Catalogo");
		}

		[HttpGet]
		[Route("login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
		{
			if (!ModelState.IsValid) return View(usuarioLogin);

			var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
				false, true);

			if (!result.Succeeded)
				return View(usuarioLogin);

			if (result.IsLockedOut)
				NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");

			if (!OperacaoValida()) return View(usuarioLogin);

			//Inserir Pessoa

			//await RealizarLogin(resposta);

			return RedirectToAction("Index", "Viagem");
		}

		[HttpGet]
		[Route("sair")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			//await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}

		private async Task RealizarLogin(UsuarioRespostaLogin resposta)
		{
			//var token = ObterTokenFormatado(resposta.AccessToken);

			var claims = new List<Claim>();
			//claims.Add(new Claim("JWT", resposta.AccessToken));
			//claims.AddRange(token.Claims);

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
				IsPersistent = true
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}

	}
}