using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Alexandria.Infra.Repositories;
public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : IEquatable<TKey>
{
    protected ISession Session;

    public Repository(ISession session)
        => Session = session;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await Session.SaveAsync(entity, cancellationToken);

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken)
        => await Session.Query<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
}
