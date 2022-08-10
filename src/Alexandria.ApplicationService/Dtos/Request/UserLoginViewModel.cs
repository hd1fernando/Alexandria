using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório")]
    [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    public string? Password { get; set; }

}

