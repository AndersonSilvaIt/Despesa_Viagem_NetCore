using DespViagem.Business.Interfaces;
using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Interfaces.services;
using DespViagem.Business.Models.Gerencial;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespViagem.Business.Services.gerencial
{
    public class PerfilUsuarioService : BaseService, IPerfilUsuarioService
    {
        private readonly IPerfilUsuarioRepository _perfilUsuarioRepository;
        private readonly IUnitOfWork _uow;

        public PerfilUsuarioService(IPerfilUsuarioRepository perfilUsuarioRepository, IUnitOfWork uow, INotificador notificador) : base(notificador, uow)
        {
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _uow = uow;
        }

        public async Task<IEnumerable<PerfilUsuario>> Get()
        {
            try
            {
                var list = await _perfilUsuarioRepository.ObterTodos();

                return list;
            }
            catch (Exception ex) { }

            return null;
        }

        public async Task<PerfilUsuario> GetById(int id)
        {
            return await _perfilUsuarioRepository.ObterPorId(id);
        }

        public async Task Insert(PerfilUsuario perfilUsuario)
        {
            await _perfilUsuarioRepository.Adicionar(perfilUsuario);

            //await _uow.Commit();
        }

        public async Task Update(PerfilUsuario perfilUsuario)
        {
            await _perfilUsuarioRepository.Atualizar(perfilUsuario);
            //await _uow.Commit();
        }

        public async Task Delete(int id)
        {
            await _perfilUsuarioRepository.Remover(id);
            //await _uow.Commit();
        }

        public async Task<bool> Commited()
        {
            return await Commit();
        }

        public Task<bool> Import(List<PerfilUsuario> perfilUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<PerfilUsuario> GetByIdWithScreens(int id)
        {
            return await _perfilUsuarioRepository.GetByIdWithScreens(id);
        }

    }
}
