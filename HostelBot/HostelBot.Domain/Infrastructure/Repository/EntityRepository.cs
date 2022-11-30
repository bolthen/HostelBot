namespace HostelBot.Domain.Infrastructure.Repository;

public class EntityRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
    where TEntity : Entity<TEntity, TId>
{
    public Task<TEntity?> GetAsync(TId key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(TId key)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}