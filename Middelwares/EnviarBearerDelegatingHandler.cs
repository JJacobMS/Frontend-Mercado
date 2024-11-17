using System.Security.Claims;

public class EnviarBearerDelegatingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization","Bearer "+httpContextAccessor.HttpContext?.User.FindFirstValue("jwt"));
        Console.WriteLine(request.Headers);
        return base.SendAsync(request, cancellationToken);
    }
}
