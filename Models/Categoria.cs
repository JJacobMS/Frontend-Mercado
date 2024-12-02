using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Categoria
{
    [Display(Name = "Id")]
    public int? CategoriaId { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Nombre { get; set; }
    [Display(Name = "Eliminable")]
    public bool Protegida { get; set; } = false;
}