using System;
using System.Threading.Tasks;

namespace DespViagem.Business.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
