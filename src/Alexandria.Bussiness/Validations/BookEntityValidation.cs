using Alexandria.Bussiness.Entitties;
using FluentValidation;

namespace Alexandria.Bussiness.Validations;
public class BookEntityValidation : AbstractValidator<BookEntity>
{
    public BookEntityValidation()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("O títilo do livro é obrigatório");

    }
}
