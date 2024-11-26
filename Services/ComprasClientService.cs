using frontendnet.Models;

namespace frontendnet.Services;

public class ComprasClientService(HttpClient client)
{
    public async Task<List<Compra>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Compra>>("api/compras");
    }
    public async Task<Compra?> GetAsync(int id)
    {
        return await client.GetFromJsonAsync<Compra>($"api/compras/{id}");
    }
}
