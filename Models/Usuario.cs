using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Usuario
{
    [Display(Name = "Id")]
    public required string Id {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "El campo {0} no es correo valido.")]
    public required string Email {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public required string Nombre {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public required string Rol {get;set;}
}