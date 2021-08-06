using DespViagem.UI.ViewModels.App;
using System.Threading.Tasks;

namespace DespViagem.UI.Services
{
    public interface IIdentityService
    {
        Task<bool> Insert(DVUserVM dvUser);
        Task<bool> Update(DVUserVM dvUser);
        Task<bool> Delete(string identityID);
        Task<DVUserVM> GetById(int id);
    }
}
