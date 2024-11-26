using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;

public class Compra
{
    public int? CompraId { get; set; }
    public DateTime FechaPedido { get; set; }
    public string? UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public List<CompraProducto>? CompraProducto { get; set; }
    public int? TotalProductos { get; set; }
    public decimal TotalCosto { get; set; }
}