
using InventarioApiJwt.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApiJwt.Conexao
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
