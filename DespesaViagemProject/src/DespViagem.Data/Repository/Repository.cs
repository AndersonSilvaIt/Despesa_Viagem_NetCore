using DespViagem.Business.Interfaces;
using DespViagem.Business.Models;
using DespViagem.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DespViagem.Data.Repository
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
	{
		protected readonly ViagemContext Db;
		protected readonly DbSet<TEntity> DbSet;
		public Repository(ViagemContext db)
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual async Task<TEntity> ObterPorId(Guid id)
		{
			return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public virtual async Task<IEnumerable<TEntity>> ObterTodos()
		{

			return await DbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
			await SaveChanges();
		}

		public virtual async Task Atualizar(TEntity entity)
		{
			DbSet.Update(entity);
			await SaveChanges();
		}

		public virtual async Task Remover(Guid id)
		{
			var entity = new TEntity { Id = id };
			DbSet.Remove(entity);
			await SaveChanges();
		}

		public async Task<int> SaveChanges()
		{
			return await Db.SaveChangesAsync();
		}

		public void Dispose()
		{
			Db?.Dispose();
		}

	}
}
