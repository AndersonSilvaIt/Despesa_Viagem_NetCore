using DespViagem.Business.Interfaces;
using DespViagem.Business.Interfaces.repositories;
using DespViagem.Business.Interfaces.services;
using DespViagem.Business.Models.Gerencial;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DespViagem.Business.Services.gerencial
{
    public class DVUserService : BaseService,  IDVUserService
    {
        private readonly IDVUserRepository _dvUserRepository;
        private readonly IUnitOfWork _uow;
        //INotificador notificador
        public DVUserService(IDVUserRepository dvUserRepository, IUnitOfWork uow,
            INotificador notificador) : base(notificador, uow)
        {
            _dvUserRepository = dvUserRepository;
            _uow = uow;
        }

        public async Task<IEnumerable<DVUser>> Get()
        {
            try
            {
                var list = await _dvUserRepository.ObterTodos();

                return list;
            }
            catch (Exception ex) { }

            return null;
        }

        public async Task<DVUser> GetById(int id)
        {
            return await _dvUserRepository.ObterPorId(id);
        }

        public async Task Insert(DVUser pwUser)
        {
            await _dvUserRepository.Adicionar(pwUser);

            //await _uow.Commit();
        }

        public async Task Update(DVUser pwUser)
        {
            await _dvUserRepository.Atualizar(pwUser);
            //await _uow.Commit();
        }

        public async Task Delete(int id)
        {
            await _dvUserRepository.Remover(id);
            //await _uow.Commit();
        }

        public async Task<bool> Commited()
        {
            return await Commit();
        }

        public Task<bool> Import(List<DVUser> pwUser)
        {
            throw new NotImplementedException();
        }

    }
}
