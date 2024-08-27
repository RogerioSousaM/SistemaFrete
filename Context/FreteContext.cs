using Microsoft.EntityFrameworkCore;
using ProvaPratica.Models;
using ProvaPratica.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProvaPratica.Context
{
    public class FreteContext : IdentityDbContext<Usuario>
    {
        public FreteContext(DbContextOptions<FreteContext> options) : base(options) 
        {

        }

        public DbSet<Frete> Fretes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        
    }
}