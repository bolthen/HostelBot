using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Repository;

public class EntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    private readonly MainDbContext context;
    
    public EntityRepository(MainDbContext context)
    {
        this.context = context;
    }
    
    public async Task<TEntity?> GetAsync(int id)
    {
        var foundEntity = await context.Set<TEntity>().FindAsync(id);

        if (foundEntity != null)
            context.Entry(foundEntity).State = EntityState.Detached;
            
        return foundEntity;
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

    public async Task<bool> DeleteAsync(int id)
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