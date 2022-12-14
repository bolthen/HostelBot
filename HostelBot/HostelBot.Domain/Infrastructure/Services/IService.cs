using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public interface IService<TEntity>
    where TEntity : Entity<TEntity>
{
    Task<bool> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(int id);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}