using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class LoanViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public int BookId { get; set; }

    [Range(1, 60, ErrorMessage = "O {0} deve estar entre {1} e {2}.")]
    public int DaysOfLoan { get; set; }
}