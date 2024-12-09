using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace frontendnet.Models;

public class MisCompra
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("fechapedido")]
    public DateTime FechaPedido { get; set; }

    [JsonPropertyName("totalPrecio")]
    public decimal TotalPrecio { get; set; }
    
    [JsonPropertyName("totalCantidad")]
    public int TotalCantidad { get; set; }
}