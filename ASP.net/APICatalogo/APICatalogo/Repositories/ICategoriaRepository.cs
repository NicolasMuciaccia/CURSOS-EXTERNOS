using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories
{
    // Ele herda todas as assinaturas do IRepository, e não adiciona nada específico a mais
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParams);
    }
}
