using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Models.Gerencial;
using DespViagem.Data.Contexto;

namespace DespViagem.Data.Repository.Gerencial
{
    public class DVUserRepository : BaseRepository<DVUser>, IDVUserRepository
    {
        public DVUserRepository(ViagemContext contexto) : base(contexto)
        {
        }
    }
}
