using DespViagem.Business.Interfaces.services;
using DespViagem.UI.ViewModels.App;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DespViagem.UI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPerfilUsuarioService _perfilUsuarioService;
        //IPWUserService
        private readonly IDVUserService _dvUserService;

        public IdentityService(SignInManager<IdentityUser> signInManager,
                                   UserManager<IdentityUser> userManager,
                                   IPerfilUsuarioService perfilUsuarioService,
                                   IDVUserService dvUserService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _perfilUsuarioService = perfilUsuarioService;
            _dvUserService = dvUserService;
        }

        public async Task<bool> Insert(DVUserVM userRegister)
        {
            var user = new IdentityUser
            {
                UserName = userRegister.UserName
                //Email = "teste@prodwin.com",
                //EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (!result.Succeeded) return false;

            if (result.Succeeded) await InsertClaimsByUser(user, userRegister.UserPerfilId);

            return true;
        }

        public async Task<bool> Update(DVUserVM userRegister)
        {
            var user = await _userManager.FindByIdAsync(userRegister.IdentityRerefenceId.ToString());

            if (user != null)
            {
                if (userRegister.ChangePassword)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, userRegister.Password);
                    // implementar mensagens de erro
                }

                if (user.UserName != userRegister.UserName)
                {
                    user.UserName = userRegister.UserName;
                    var result5 = await _userManager.UpdateAsync(user);
                    // implementar mensagens de erro
                }
                await RemoveClaimsByUser(user);
                await InsertClaimsByUser(user, userRegister.UserPerfilId);
            }

            return true;
        }

        private async Task InsertClaimsByUser(IdentityUser identityUser, int userPerfilId)
        {
            var userPerfil = await _perfilUsuarioService.GetByIdWithScreens(userPerfilId);
            if (userPerfil != null)
            {
                IList<Claim> claims = new List<Claim>();
                foreach (var item in userPerfil.Telas)
                {
                    if (item.Insert) claims.Add(new Claim(item.Description, "Insert"));
                    if (item.Update) claims.Add(new Claim(item.Description, "Update"));
                    if (item.Delete) claims.Add(new Claim(item.Description, "Delete"));
                    if (item.View) claims.Add(new Claim(item.Description, "View"));
                    if (item.List) claims.Add(new Claim(item.Description, "List"));
                    if (item.Import) claims.Add(new Claim(item.Description, "Import"));
                    if (item.Export) claims.Add(new Claim(item.Description, "Export"));
                }
                if (claims.Any()) await _userManager.AddClaimsAsync(identityUser, claims);
            }
        }

        private async Task RemoveClaimsByUser(IdentityUser identityUser)
        {
            var claims = await _userManager.GetClaimsAsync(identityUser);
            if (claims.Any()) await _userManager.RemoveClaimsAsync(identityUser, claims);
        }

        public async Task<bool> Delete(string identityID)
        {
            var user = await _userManager.FindByIdAsync(identityID);

            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<DVUserVM> GetById(int id)
        {

            return null;
        }

    }
}
