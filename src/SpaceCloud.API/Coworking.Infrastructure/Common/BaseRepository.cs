using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coworking.Application.Interfaces;
using Coworking.Domain;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly WorkingContext Context;

        protected BaseRepository(WorkingContext context)
        {
            Context = context;
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await FindAsync(id);
            if (entity is null)
                throw new NotFoundException(id.ToString(), HttpMethod.Delete,
                    $"element with primary key {id} cannot be found!");
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity?> FindAsync(int id)
        {
            try
            {
                var entity = await Context.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}