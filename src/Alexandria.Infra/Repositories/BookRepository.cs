using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Alexandria.Infra.Repositories;

public class BookRepository : Repository<BookEntity, int>, IBookRepository
{
    public BookRepository(ISession session) : base(session)
    {
    }

    public Task<BookEntity> FindBookByISBN(string isbn, CancellationToken cancellationToken)
        => Session.Query<BookEntity>()
            .FirstOrDefaultAsync(x => x.ISBN == isbn, cancellationToken);
}
