
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProvaPratica.Context;
using ProvaPratica.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext para usar SQL Server
builder.Services.AddDbContext<FreteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddIdentity<Usuario, IdentityRole>()
        .AddEntityFrameworkStores<FreteContext>()
        .AddDefaultTokenProviders();    

// Adicionar serviços ao contêiner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de solicitação HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
