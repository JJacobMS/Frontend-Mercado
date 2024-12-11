using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class UsuarioPwd
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [Display(Name = "Correo electronico")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).{8,}$",
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayúscula, un número y un carácter especial.")]
    public required string Password { get; set; }
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "El campo {0} no puede contener solo espacios.")]

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public required string Nombre { get; set; }
}