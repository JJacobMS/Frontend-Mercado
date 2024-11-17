using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace frontendnet.Models;

public class Carrito
{
    [JsonPropertyName("carritoId")]
    public int? Id { get; set; }
    [JsonPropertyName("total")]
    public decimal? TotalCompra { get; set; }

    [JsonPropertyName("carritoproducto")]
    public List<CarritoProducto>? CarritoProductos { get; set; }
}