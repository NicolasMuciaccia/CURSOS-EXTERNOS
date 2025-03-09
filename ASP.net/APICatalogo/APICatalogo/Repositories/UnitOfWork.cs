using APICatalogo.Context;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepo;  // Apenas defini as propriedades, tem q definir elas

        private ICategoriaRepository _categoriaRepo;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository 
        {                                           
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }
        /*
         Para evitar a criação de diversas instâncias no código, implementasse aqui dessa forma para que ele crie uma nova instância
        somente se _produtoRepo for nulo "??", se não for, ele só chama o get normalmente.
        */
        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public void Commit()  // cria persistência no banco
        {
            _context.SaveChanges();
        }

        public void Dispose() // ele libera alguns recursos não gerenciados
        {
            _context.Dispose();
        }
    }
}
