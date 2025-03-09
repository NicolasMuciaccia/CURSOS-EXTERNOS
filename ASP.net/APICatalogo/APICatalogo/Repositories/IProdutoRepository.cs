using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    // Ele herda todas as assinaturas do IRepository, e adiciona uma assinatura específica de produto
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParams);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutoFiltroPreco produtosFiltroParams);
    }
}
