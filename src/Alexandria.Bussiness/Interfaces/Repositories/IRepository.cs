using Alexandria.Bussiness.Entitties;

namespace Alexandria.Bussiness.Intefaces.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken);
}
