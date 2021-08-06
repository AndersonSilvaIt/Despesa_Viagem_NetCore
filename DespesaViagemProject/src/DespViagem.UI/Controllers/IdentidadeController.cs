using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DespViagem.Business.Interfaces;
using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Interfaces.services;
using DespViagem.Business.Models;
using DespViagem.Business.Models.DomainObjects;
using DespViagem.Business.Models.Gerencial;
using DespViagem.UI.Models;
using DespViagem.UI.Services;
using DespViagem.UI.ViewModels;
using DespViagem.UI.ViewModels.App;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DespViagem.UI.Controllers
{
    public class IdentidadeController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IIdentityService _identityService;
		private readonly IDVUserService _dvUserService;
		private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;

		//perfil Usuario Repository
		private readonly IPessoaService _pessoaService;

		private readonly int pageSize = 4;

		public IdentidadeController(INotificador notificador,
							IIdentityService identityService,
							  SignInManager<IdentityUser> signInManager,
								   UserManager<IdentityUser> userManager,
								   IPessoaService pessoaService, IMapper mapper,
								   IPerfilUsuarioRepository perfilUsuarioRepository) 
			: base(notificador)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_pessoaService = pessoaService;
			_perfilUsuarioRepository = perfilUsuarioRepository;
			_identityService = identityService;
		}

		//[ClaimsAuthorize("User", "List")]
		public async Task<IActionResult> Index(int? pageNumber)
		{
			var lista = _mapper.Map<IEnumerable<DVUserVM>>(await _dvUserService.Get());

			return View(PaginatedList<DVUserVM>.Create(lista.AsQueryable(), pageNumber ?? 1, pageSize));
		}

		[HttpGet()]
		//[Route("new-account"), HttpGet(), ClaimsAuthorize("User", "Insert")]
		public async Task<IActionResult> Create()
		{
			var userPW = await PopulateUserPerfil(new DVUserVM());
			//userPW.UserName = "";
			//userPW.Pass = "";
			//return View("Create", userPW);
			return View("Create", userPW);
		}

		[HttpPost()]
		public async Task<IActionResult> Create(DVUserVM userPW)
		{
			ModelState.Remove("Code");
			ModelState.Remove("Description");
			ModelState.Remove("UserPerfilId");
			ModelState.Remove("ConfirmPassword");
			ModelState.Remove("ChangePassword");

			if (!ModelState.IsValid) return View(userPW);

			//if (!Email.Validar(usuarioRegistro.Email))
			//{
			//	NotificarErro("E-mail inválido");
			//
			//	return View(usuarioRegistro);
			//}

			var result = await _identityService.Insert(userPW);

			if (!result)
			{
				//foreach (var error in result.Errors)
				//	NotificarErro(error.Description);

				return View(userPW);
			}

			await _dvUserService.Insert(_mapper.Map<DVUser>(userPW));
			await _dvUserService.Commited();

			var url = Url.Action("Index", "Identity");
			return Json(new { success = true, url });
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var pwUser = _mapper.Map<DVUserVM>(await _dvUserService.GetById(id));

			pwUser = await PopulateUserPerfil(pwUser);

			return View("Details", pwUser);
		}

		[HttpGet()]
		public async Task<IActionResult> Edit(int id)
		{
			var pwUser = _mapper.Map<DVUserVM>(await _dvUserService.GetById(id));

			pwUser = await PopulateUserPerfil(pwUser);
			pwUser.Password = "********";

			return View("Edit", pwUser);
		}

		public async Task<IActionResult> Edit(DVUserVM userPW)
		{
			ModelState.Remove("Code");
			ModelState.Remove("Description");
			ModelState.Remove("UserPerfilId");
			ModelState.Remove("ConfirmPassword");
			ModelState.Remove("UserPerfils");

			if (!userPW.ChangePassword) ModelState.Remove("Password");

			if (!ModelState.IsValid) return View(userPW);

			//if (!Email.Validar(usuarioRegistro.Email))
			//{
			//	NotificarErro("E-mail inválido");
			//
			//	return View(usuarioRegistro);
			//}

			var result = await _identityService.Update(userPW);

			if (!result)
			{
				//foreach (var error in result.Errors)
				//	NotificarErro(error.Description);

				return View(userPW);
			}

			await _dvUserService.Update(_mapper.Map<DVUser>(userPW));
			await _dvUserService.Commited();

			var url = Url.Action("Index", "Identity");
			return Json(new { success = true, url });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var pwUser = _mapper.Map<DVUserVM>(await _dvUserService.GetById(id));

			pwUser = await PopulateUserPerfil(pwUser);
			pwUser.Password = "********";

			return View("Delete", pwUser);
		}

		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(DVUserVM dvUserVM)
		{
			var deleted = await _identityService.Delete(dvUserVM.IdentityRerefenceId.ToString());

			if (!deleted) return View(dvUserVM);

			await _dvUserService.Delete(dvUserVM.Id);
			await _dvUserService.Commited();

			var url = Url.Action("Index", "Identity");
			return Json(new { success = true, url });
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
			//pessoa.Id = user.Id;
			pessoa.Documento = usuarioRegistro.CPF;
			pessoa.Idade = 10;
				
			await _pessoaService.Adicionar(pessoa);

			if(!OperacaoValida())
				return View(usuarioRegistro);


			return RedirectToAction("Identidade", "Catalogo");
		}

		[HttpGet()]
		[Route("login")]
		//[AllowAnonymous, HttpGet, Route("login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost()]
		//[AllowAnonymous, HttpPost, Route("login")]
		[Route("login")]
		public async Task<IActionResult> Login(UserLogin usuarioLogin)
		{
			if (!ModelState.IsValid) return View(usuarioLogin);

			var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.PwdLogin,
				false, true);

			if (!result.Succeeded)
				return View(usuarioLogin);

			//if (result.IsLockedOut)
			//	NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
			//
			//if (!OperacaoValida()) return View(usuarioLogin);

			//Inserir Pessoa

			//await RealizarLogin(resposta);

			//HttpContext.SignInAsync()

			return RedirectToAction("Index", "Home");
		}

		/*[HttpGet]
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
		*/
		/*[HttpGet]
		[Route("sair")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			//await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}
		*/
		[HttpGet, Route("exit")]
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

		private async Task<DVUserVM> PopulateUserPerfil(DVUserVM pwUser)
		{
			pwUser.PerfilUsuario = _mapper.Map<IEnumerable<PerfilUsuarioVM>>(await _perfilUsuarioRepository.ObterTodos());
			return pwUser;
		}

	}
}