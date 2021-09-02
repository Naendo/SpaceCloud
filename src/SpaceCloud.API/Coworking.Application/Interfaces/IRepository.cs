using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Domain;

namespace Coworking.Application.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAsync();

        Task DeleteAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(TEntity id);
        Task<TEntity> FindAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}