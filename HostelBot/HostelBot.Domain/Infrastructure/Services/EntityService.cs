using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public abstract class EntityService<TEntity> : IService<TEntity>
    where  TEntity : Entity<TEntity>
{
    private protected EntityRepository<TEntity> repository;

    protected EntityService(EntityRepository<TEntity> repository)
    {
        this.repository = repository;
    }
    
    public async Task<bool> CreateAsync(TEntity entity)
    {
        var createSuccessful = await repository.CreateAsync(entity);

        if (!createSuccessful)
            throw new Exception($"This {typeof(TEntity)} has already been added to the database");

        return createSuccessful;
    }

    public async Task<TEntity> GetAsync(int id)
    {
        var entity = await repository.GetAsync(id);

        if (entity == null)
            throw new Exception($"The {typeof(TEntity)} with the given id was not found in the database");

        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var updateSuccessful = await repository.UpdateAsync(entity);

        if (!updateSuccessful)
            throw new Exception($"This {typeof(TEntity)} was not found in the database");

        return updateSuccessful;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteSuccessful = await repository.DeleteAsync(id);
        
        if (!deleteSuccessful)
            throw new Exception($"This {typeof(TEntity)} was not found in the database");

        return deleteSuccessful;
    }
}