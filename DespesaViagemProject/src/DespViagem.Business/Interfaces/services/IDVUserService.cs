using DespViagem.Business.Models.Gerencial;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces.services
{
    public interface IDVUserService
    {
        Task Insert(DVUser dvUser);
        Task Update(DVUser dvUser);
        Task Delete(int id);
        Task<DVUser> GetById(int id);
        Task<IEnumerable<DVUser>> Get();
        Task<bool> Commited();
        Task<bool> Import(List<DVUser> dvUser);
    }
}
