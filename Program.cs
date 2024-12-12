using frontendnet.Middlewares;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

var cultureInfo = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var UrlWebAPI = builder.Configuration["UrlWebAPI"];
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<EnviarBearerDelegatingHandler>();
builder.Services.AddTransient<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<AuthClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>();

builder.Services.AddHttpClient<CategoriasClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<UsuariosClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<RolesClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<ProductosClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<PerfilClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<ArchivosClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<BitacoraClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
    .AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
    .AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<CarritosClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
.AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();


builder.Services.AddHttpClient<ComprasClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
.AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddHttpClient<MisComprasClientService>(httpClient => { httpClient.BaseAddress = new Uri(UrlWebAPI!); })
.AddHttpMessageHandler<EnviarBearerDelegatingHandler>()
.AddHttpMessageHandler<RefrescaTokenDelegatingHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = ".frontendnet";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.LoginPath = "/Auth";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFoundPage";
        await next();
    }
});

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
