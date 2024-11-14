using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Upload
{
    [Display(Name = "Id")]
    public int? ArchivoId {get;set;}
    public string? Nombre {get;set;}

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Upload)]
    [Display(Name = "Portada")]
    public required IFormFile Portada {get;set;}   
}