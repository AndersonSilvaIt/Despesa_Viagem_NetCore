using DespViagem.Business.Models.Gerencial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces.services
{
    public interface IPerfilUsuarioService
    {
        Task Insert(PerfilUsuario perfilUsuario);
        Task Update(PerfilUsuario perfilUsuario);
        Task Delete(int id);
        Task<PerfilUsuario> GetById(int id);
        Task<PerfilUsuario> GetByIdWithScreens(int id);
        Task<IEnumerable<PerfilUsuario>> Get();
        Task<bool> Commited();
        Task<bool> Import(List<PerfilUsuario> perfilUsuario);
    }
}
