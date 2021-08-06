using DespViagem.Business.Models.Gerencial;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces.repositories
{
    public interface IPerfilUsuarioRepository : IRepository<PerfilUsuario>
    {
        Task<PerfilUsuario> GetByIdWithScreens(int id);
    }
}
