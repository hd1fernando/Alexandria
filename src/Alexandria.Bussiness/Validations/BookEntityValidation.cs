using Alexandria.Bussiness.Entitties;
using FluentValidation;

namespace Alexandria.Bussiness.Validations;
public class BookEntityValidation : AbstractValidator<BookEntity>
{
    public BookEntityValidation()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} é obrigatório");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("O {PropertyName} deve ser maior do que {PropertyValue}");
        RuleFor(x => x.ISBN).NotEmpty().WithMessage("{PropertyName} é obrigatório");
    }
}
