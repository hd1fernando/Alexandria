using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;
using Alexandria.Bussiness.Validations;

namespace Alexandria.Bussiness.Services;

public class BookService : BaseService, IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(INotifier notifier, IBookRepository bookRepository) : base(notifier)
    {
        _bookRepository = bookRepository;
    }

    public async Task CreateBookAsync(BookEntity book, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(book, nameof(book));

        if (await DoValidationAsync<BookEntityValidation, BookEntity, int>(new BookEntityValidation(_bookRepository), book) == false)
            return;

        await _bookRepository.AddAsync(book, cancellationToken);
    }
}
