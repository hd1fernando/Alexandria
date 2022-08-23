using Alexandria.ApplicationService.Dtos.Anottations;
using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class BookInstanceViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string? ISBN { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    [ValidCirculationType(ErrorMessage = "O tipo de circulação informado é inválido.")]
    public string? CiculationType { get; set; }
}

