
using InventarioApiJwt.Conexao;
using InventarioApiJwt.Models;

namespace InventarioApiJwt.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(InventarioContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
