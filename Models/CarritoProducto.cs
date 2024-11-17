using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace frontendnet.Models;

public class CarritoProducto
{

    public int? CarritoId { get; set; }
    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }
    [JsonPropertyName("totalprecio")]
    public decimal? TotalPrecio { get; set; }
    public Producto? Producto { get; set; }

}