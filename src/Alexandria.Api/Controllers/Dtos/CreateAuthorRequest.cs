using System.ComponentModel.DataAnnotations;

namespace Alexandria.Api.Controllers.Dtos;

public class CreateAuthorRequest
{
    [Required(ErrorMessage = "{0} is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [MaxLength(400, ErrorMessage = "{0} must be less than {1} characters")]
    public string? Description { get; set; }
}
