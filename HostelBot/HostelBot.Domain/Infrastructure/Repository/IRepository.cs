namespace HostelBot.Domain.Infrastructure.Repository;

public interface IEntityRepository<TEntity, TId>
    where TEntity : Entity<TEntity, TId>
{
    Task<TEntity?> GetAsync(TId id);
        
    Task<bool> CreateAsync(TEntity entity);
        
    Task<bool> DeleteAsync(TId id);

    Task<bool> UpdateAsync(TEntity entity);

}