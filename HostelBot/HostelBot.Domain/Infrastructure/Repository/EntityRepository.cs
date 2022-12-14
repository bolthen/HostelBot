using Microsoft.EntityFrameworkCore;

namespace HostelBot.Domain.Infrastructure.Repository;

public class EntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    public MainDbContext Context;
    
    public EntityRepository(MainDbContext context)
    {
        this.Context = context;
    }
    public async Task<TEntity?> GetAsync(int id)
    {
        var foundEntity = await Context.Set<TEntity>().FindAsync(id);

        if (foundEntity != null)
            Context.Entry(foundEntity).State = EntityState.Detached;
            
        return foundEntity;
    }

    public async Task<bool> CreateAsync(TEntity entity)
    {
        var foundEntity = await Context.Set<TEntity>().FindAsync(entity.Id);

        if (foundEntity != null)
            return false;

        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var foundEntity = await Context.Set<TEntity>().FindAsync(id);

        if (foundEntity == null)
            return false;

        Context.Set<TEntity>().Remove(foundEntity);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var foundEntity = await Context.Set<TEntity>().FindAsync(entity.Id);

        if (foundEntity == null)
            return false;

        foundEntity = entity;

        Context.Entry(foundEntity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return true;
    }
}