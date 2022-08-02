namespace Alexandria.Bussiness.Entitties;
public abstract class Entity<TKey>
{
    public virtual TKey Id { get; protected set; }
}
