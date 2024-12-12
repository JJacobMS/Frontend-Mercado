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
        var response = await client.GetAsync($"api/usuarios/{email}");
        response.EnsureSuccessStatusCode();
        var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
        return usuario;
    }
/*
    public async Task<Usuario?> GetAsync(string email)
    {
        var response = await client.GetFromJsonAsync<Usuario>($"api/usuarios/{email}");
        return response;
    }*/
    public async Task<HttpResponseMessage> PostAsync(UsuarioPwd usuario)
    {
        var response = await client.PostAsJsonAsync("api/usuarios", usuario);
        response.EnsureSuccessStatusCode();
        return response;
    }
    public async Task<HttpResponseMessage> PutAsync(Usuario usuario)
    {
        var response = await client.PutAsJsonAsync($"api/usuarios/{usuario.Email}", usuario);
        response.EnsureSuccessStatusCode();
        return response;
    }
    public async Task<HttpResponseMessage> DeleteAsync(string email)
    {
        var response = await client.DeleteAsync($"api/usuarios/{email}");
        response.EnsureSuccessStatusCode();
        return response;
    }
}