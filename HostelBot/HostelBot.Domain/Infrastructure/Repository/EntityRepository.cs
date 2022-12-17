using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Repository;

public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    protected readonly MainDbContext context;
    
    public EntityRepository(MainDbContext context)
    {
        this.context = context;
    }
    
    public async Task<TEntity> GetAsync(long id)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(id);

        if (foundEntity != null) return foundEntity;
        
        context.Entry(foundEntity).State = EntityState.Detached;
        throw new ArgumentException($"The {typeof(TEntity)} with the given id({id}) was not found in the database");
    }
    
    public async Task<bool> CheckAsync(long id)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(id);

        return foundEntity != null;
    }

    public async Task<bool> CreateAsync(TEntity entity)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(entity.Id);

        if (foundEntity != null)
            return false;

        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(id);

        if (foundEntity == null)
            return false;

        context.Set<TEntity>().Remove(foundEntity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(entity.Id);

        if (foundEntity == null)
            return false;

        foundEntity = entity;

        context.Entry(foundEntity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return true;
    }
}