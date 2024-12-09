using frontendnet.Models;
namespace frontendnet.Services;

public class UsuariosClientService(HttpClient client)
{
    public async Task<List<Usuario>?> GetAsync()
    {
        return await client.GetFromJsonAsync<List<Usuario>>($"api/usuarios");
    }
    public async Task<Usuario?> GetAsync(string email)
    {
        return await client.GetFromJsonAsync<Usuario>($"api/usuarios/{email}");
    }
    public async Task<HttpResponseMessage> PostAsync(UsuarioPwd usuario)
    {
        var response = await client.PostAsJsonAsync("api/usuarios", usuario);
        return response;
    }
    public async Task<HttpResponseMessage> PutAsync(Usuario usuario)
    {
        var response = await client.PutAsJsonAsync($"api/usuarios/{usuario.Email}", usuario);
        return response;
    }
    public async Task<HttpResponseMessage> DeleteAsync(string email)
    {
                Console.WriteLine("ELIMINANDO7");
        var response = await client.DeleteAsync($"api/usuarios/{email}");
        return response;
    }
}