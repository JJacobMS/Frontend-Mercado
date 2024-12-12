using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class UsuarioPwd
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [Display(Name = "Correo electronico")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>[\]\\/_=;:.-]).{8,}$",
    ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayúscula, un número y un carácter especial.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Password { get; set; }
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El campo {0} no puede contener solo espacios.")]

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Nombre { get; set; }
}