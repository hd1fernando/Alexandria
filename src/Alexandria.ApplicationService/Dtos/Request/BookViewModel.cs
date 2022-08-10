using Alexandria.Bussiness.Entitties;
using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class BookViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? Title { get; set; }

    [Range(1d, double.MaxValue, ErrorMessage = "{0} deve ser maior ou igual a {1}")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? ISBN { get; set; }

    public BookEntity ToEntity()
        => new BookEntity(Title, Price, ISBN);

}

