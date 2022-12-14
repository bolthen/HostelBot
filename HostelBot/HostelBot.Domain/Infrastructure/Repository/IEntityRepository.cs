namespace HostelBot.Domain.Infrastructure.Repository;

public interface IEntityRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    Task<TEntity?> GetAsync(int id);
        
    Task<bool> CreateAsync(TEntity entity);
        
    Task<bool> DeleteAsync(int id);

    Task<bool> UpdateAsync(TEntity entity);

}