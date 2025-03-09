using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    // Terá apenas lógica de acesso a dados, sem regras de negócios (regras ficam no controlador)
    {
        // Herda o context da base (Repository)
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParams)
        {
            var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();
            var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, categoriaParams.PageNumber, categoriaParams.PageSize);
            return categoriasOrdenadas;
        }
    }
}
