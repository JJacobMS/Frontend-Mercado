using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace frontendnet.Models;

public class Producto
{
    [Display(Name = "Id")]
    public int? ProductoId { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    public required string Titulo { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [StringLength(65535, MinimumLength = 1, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    [DataType(DataType.MultilineText)]
    public string Descripcion { get; set; } = "Sin descripción";

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^\d+(\.\d{0,2})$", ErrorMessage = "El valor del campo debe ser un precio válido.")]
    [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
    [Display(Name = "Precio")]
    public decimal Precio { get; set; }

    [Display(Name = "Portada")]
    public int? ArchivoId { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "Sólo números positivos")]
    public int? CantidadDisponible { get; set; }

    [Display(Name = "Eliminable")]
    public bool Protegida { get; set; } = false;

    public ICollection<Categoria>? Categorias { get; set; }
    public ICollection<CompraProducto>? CompraProducto { get; set; }
}
