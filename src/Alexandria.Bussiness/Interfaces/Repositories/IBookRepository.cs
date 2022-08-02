using Alexandria.Bussiness.Entitties;

namespace Alexandria.Bussiness.Intefaces.Repositories;

public interface IBookRepository : IRepository<BookEntity, int>
{
    public Task<BookEntity> FindBookByISBN(string isbn, CancellationToken cancellationToken);
}
