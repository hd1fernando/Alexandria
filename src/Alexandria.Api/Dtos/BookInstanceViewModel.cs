using System.ComponentModel.DataAnnotations;

namespace Alexandria.Api.Dtos;

public class BookInstanceViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? ISBN { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? CiculationType { get; set; }
}

