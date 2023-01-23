namespace HostelBot.Domain.Infrastructure.Repository;

public interface IEntityRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    Task<TEntity> GetAsync(long id);
        
    Task<bool> CreateAsync(TEntity entity);
        
    Task<bool> DeleteAsync(long id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> CheckAsync(long id);
}