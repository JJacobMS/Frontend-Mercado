using frontendnet.Models;

namespace frontendnet.Services;

public class MisComprasClientService(HttpClient client)
{
    public async Task<List<MisCompra>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<MisCompra>>("api/compras/personal");
    }
    public async Task<List<MisCompraDetalle>?> GetAsync(int id)
    {
        var misCompraDetalle = await client.GetFromJsonAsync<List<MisCompraDetalle>>($"api/compras/personal/{id}");
        return misCompraDetalle;
    }
}
