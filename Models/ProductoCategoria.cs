using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class ProductoCategoria
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Display(Name = "Categoria")]
    public int? CategoriaId { get; set; }

    public string? Nombre { get; set; }

    public Producto? Producto { get; set; }

}