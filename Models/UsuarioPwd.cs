using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class UsuarioPwd
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [Display(Name = "Correo electronico")]
    public required string Email {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public required string Password {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public required string Nombre {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public required string Rol {get;set;}
}