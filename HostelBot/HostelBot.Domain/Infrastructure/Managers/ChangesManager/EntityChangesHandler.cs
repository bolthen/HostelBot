namespace HostelBot.Domain.Infrastructure.Managers;

public class EntityChangesHandler<TEntity> 
    where TEntity : Entity<TEntity>
{
    protected event Action<TEntity>? HandleChanges;

    public void AddChangesHandler(Action<TEntity> Handler)
    {
        HandleChanges += Handler;
    }

    public virtual void OnHandleChanges(TEntity obj)
    {
        HandleChanges?.Invoke(obj);
    }
}