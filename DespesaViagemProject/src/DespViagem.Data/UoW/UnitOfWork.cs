using DespViagem.Business.Interfaces;
using DespViagem.Data.Contexto;
using System;
using System.Threading.Tasks;

namespace DespViagem.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContext _context;

        public UnitOfWork(ViagemContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            int changeAmount = 0;
            try
            {
                changeAmount = await _context.SaveChangesContext();
            }
            catch (Exception ex)
            {

            }
            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
