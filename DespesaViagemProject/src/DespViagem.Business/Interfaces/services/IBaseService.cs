using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces.services
{
    public interface IBaseService
    {
        Task<bool> Commit();
    }
}
