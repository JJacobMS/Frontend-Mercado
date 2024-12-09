using System.ComponentModel.DataAnnotations;

namespace frontendnet.Models;
public class CompraDetalleViewModel
{
    public MisCompra? Compra { get; set; } 
    public List<MisCompraDetalle>? Productos { get; set; } 
}
