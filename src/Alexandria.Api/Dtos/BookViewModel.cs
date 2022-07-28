using System.ComponentModel.DataAnnotations;

namespace Alexandria.Api.Dtos;

public class BookViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? Title { get; set; }

    [Range(1d, double.MaxValue, ErrorMessage = "{0} deve existir e ser maior do que 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? ISBN { get; set; }

}

