using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProvaPratica.Models;

namespace ProvaPratica.Context
{
    public class FreteContext : IdentityDbContext<IdentityUser>
    {
        public FreteContext(DbContextOptions<FreteContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Frete> Fretes { get; set; }
    }
}