using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Intefaces.Repositories;
using FluentValidation;

namespace Alexandria.Bussiness.Validations;
public class BookEntityValidation : AbstractValidator<BookEntity>
{
    private readonly IBookRepository _bookRepository;

    public BookEntityValidation(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

        RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} é obrigatório");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("O {PropertyName} deve ser maior do que {PropertyValue}");
        RuleFor(x => x.ISBN).NotEmpty().WithMessage("{PropertyName} é obrigatório");

        ISBNIsUnique();
    }

    private void ISBNIsUnique()
    {
        RuleFor(x => x.ISBN)
            .MustAsync(async (isbn, cancellationToken) =>
            {
                var book = await _bookRepository.FindBookByISBN(isbn!, cancellationToken);
                return book is null;
            }).WithMessage("ISBN já cadastrado");
    }
}
