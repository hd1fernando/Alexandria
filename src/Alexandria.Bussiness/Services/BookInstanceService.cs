using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Interfaces.Services;
using Alexandria.Bussiness.Validations;

namespace Alexandria.Bussiness.Services;

public class BookInstanceService : BaseService, IBookInstanceService
{
    private readonly IBookRepository _bookRepository;
    private readonly IRepository<BookInstanceEntity, int> _bookInstanceRepository;

    public BookInstanceService(INotifier notifier, IBookRepository bookRepository, IRepository<BookInstanceEntity, int> bookInstanceRepository) : base(notifier)
    {
        _bookRepository = bookRepository;
        _bookInstanceRepository = bookInstanceRepository;
    }

    public async Task CreateBookInstanceAsync(string isbn, string circulationType, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.FindBookByISBN(isbn!, cancellationToken);

        var bookInstance = new BookInstanceEntity(circulationType, book);

        if (DoValidation<BookInstanceEntityValidation, BookInstanceEntity, int>(new BookInstanceEntityValidation(), bookInstance) == false)
            return;

        await _bookInstanceRepository.AddAsync(bookInstance, cancellationToken);
    }
}
