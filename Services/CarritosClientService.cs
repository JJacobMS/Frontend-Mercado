using frontendnet.Models;

namespace frontendnet.Services;

public class CarritosClientService(HttpClient client)
{
    public async Task<List<Carrito>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Carrito>?>("api/carritos");
    }
    
    public async Task DeleteAsync(int idProducto)
    {
        var response = await client.DeleteAsync($"api/carritos/{idProducto}");
        response.EnsureSuccessStatusCode();
    }
    public async Task PutAsync(int idProducto, int cantidad)
    {
        var data = new { cantidad = cantidad };
        var response = await client.PutAsJsonAsync($"api/carritos/{idProducto}", data);
        response.EnsureSuccessStatusCode();
    }
    
}
