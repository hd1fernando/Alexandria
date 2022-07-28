using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;
using Alexandria.Bussiness.Validations;

namespace Alexandria.Bussiness.Services;

public class BookService : BaseService, IBookService
{
    private readonly IRepository<BookEntity, int> _bookRepository;

    public BookService(INotifier notifier, IRepository<BookEntity, int> bookRepository) : base(notifier)
    {
        _bookRepository = bookRepository;
    }

    public async Task CreateBookAsync(BookEntity book, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (DoValidation<BookEntityValidation, BookEntity, int>(new BookEntityValidation(), book) == false)
            return;

        await _bookRepository.AddAsync(book, cancellationToken);
    }
}
