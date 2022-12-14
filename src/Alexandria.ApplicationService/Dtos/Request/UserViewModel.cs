using Alexandria.ApplicationService.Dtos.Anottations;
using System.ComponentModel.DataAnnotations;

namespace Alexandria.ApplicationService.Dtos.Request;

public class UserViewModel
{
    [Required(ErrorMessage = "{0} é obrigatório")]
    [EmailAddress(ErrorMessage = "{0} deve ser um e-mail válido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    public string? Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais")]
    public string? ConfirmPassowrd { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    [ValidUserType(ErrorMessage = "UserType inválido.")]
    public string? UserType { get; set; }

}

