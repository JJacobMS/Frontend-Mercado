using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace frontendnet.Models;

public class MisCompraDetalle
{
    [JsonPropertyName("titulo")]
    public string? Titulo { get; set; }
    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }
    [JsonPropertyName("precio")]
    public decimal Precio { get; set; }
    [JsonPropertyName("cantidad")]
    public int Cantidad { get; set; }
}