using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Models.Gerencial;
using DespViagem.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DespViagem.Data.Repository.Gerencial
{
    public class PerfilUsuarioRepository : BaseRepository<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(ViagemContext pgContext) :
                                   base(pgContext)
        { }

        public async Task<PerfilUsuario> GetByIdWithScreens(int id)
        {
            return await Db.PerfilUsuario.AsNoTracking()
                .Include(s => s.Telas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
