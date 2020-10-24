using DespViagem.UI.Data;
using DespViagem.UI.Extensions;
using DespViagem.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DespViagem.UI.Configuration
{
	public static class IdentityConfig
	{
		public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
									IConfiguration configuration)
		{
			////services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			////	.AddCookie(options =>
			////	{
			////		options.LoginPath = "/login"; // se não estiver logado, será redirecionado para a rota /login
			////		options.AccessDeniedPath = "/acesso-negado"; // se tiver o acesso negado
			////	});
			//
			//services.AddDbContext<ApplicationDbContext>(options =>
			//		options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			//
			//
			//services.AddDefaultIdentity<IdentityUser>()
			//	.AddRoles<IdentityRole>()
			//	.AddErrorDescriber<IdentityMensagemPortugues>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>()
			//	.AddDefaultTokenProviders();
			//
			//services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			//	.AddCookie(options =>
			//	{
			//		options.LoginPath = "/login"; // se não estiver logado, será redirecionado para a rota /login
			//		options.AccessDeniedPath = "/acesso-negado"; // se tiver o acesso negado
			//	});

			//services.AddJwtConfiguration(configuration);

			services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			//services.AddIdentity<ApplicationUser, IdentityRole>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>()
			//	.AddDefaultTokenProviders();

			services.AddDefaultIdentity<IdentityUser>()
				.AddRoles<IdentityRole>()
				.AddErrorDescriber<IdentityMensagemPortugues>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(150);
				// If the LoginPath isn't set, ASP.NET Core defaults 
				// the path to /Account/Login.
				options.LoginPath = "/login";
				// If the AccessDeniedPath isn't set, ASP.NET Core defaults 
				// the path to /Account/AccessDenied.
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			return services;
		}

		public static void UseIdentityConfiguration(this IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}

	}
}
