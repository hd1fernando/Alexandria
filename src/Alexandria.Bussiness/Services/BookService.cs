using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;

namespace Alexandria.Bussiness.Services;

public class BookService : BaseService, IBookService
{
    public BookService(INotifier notifier) : base(notifier)
    {
    }

    public Task CreateBook(BookEntity book)
    {
        throw new NotImplementedException();
    }
}
