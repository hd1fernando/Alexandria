using Alexandria.Bussiness.Entitties;
using FluentValidation;

namespace Alexandria.Bussiness.Validations;

public class BookInstanceEntityValidation : AbstractValidator<BookInstanceEntity>
{
    public BookInstanceEntityValidation()
    {

        RuleFor(x => x.CirculationType).NotEmpty().WithMessage("{Propertyname} é obrigatório");
        BookShouldExist();

    }

    private void BookShouldExist()
        => RuleFor(x => x.Book).NotNull().WithMessage("Esse livro não está cadastrado no sistema");
}
