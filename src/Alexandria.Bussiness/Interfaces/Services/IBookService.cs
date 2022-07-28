using Alexandria.Bussiness.Entitties;

namespace Alexandria.Bussiness.Interfaces.Services;
public interface IBookService
{
    public Task CreateBook(BookEntity book);
}
