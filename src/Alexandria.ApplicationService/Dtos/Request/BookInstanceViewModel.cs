using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class BookInstanceViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? ISBN { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? CiculationType { get; set; }
}

