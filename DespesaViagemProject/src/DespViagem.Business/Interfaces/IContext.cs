using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
    public interface IContext : IDisposable
    {
        Task<int> SaveChangesContext();
    }
}
