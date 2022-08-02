using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;

namespace Alexandria.Infra.Repositories;
public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
