using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Usuario
{
    [Display(Name = "Id")]
    public required string Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El campo {0} no puede contener solo espacios.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Nombre { get; set; }

    public string? Rol { get; set; }
}