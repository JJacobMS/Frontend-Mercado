using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace frontendnet.Models;

public class CarritoProducto
{

    public int? CarritoId { get; set; }

    [JsonPropertyName("cantidad")]
    public int? Cantidad { get; set; }
    [JsonPropertyName("totalprecio")]
    public decimal? TotalPrecio { get; set; }
    [JsonPropertyName("producto")]
    public Producto? Producto { get; set; }

}