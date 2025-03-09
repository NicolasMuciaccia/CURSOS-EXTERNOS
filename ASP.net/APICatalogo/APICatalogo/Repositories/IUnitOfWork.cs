namespace APICatalogo.Repositories
{
    public interface IUnitOfWork
    {
        // Abstrai os repositórios e faz a parte de commit/rollback, não usaremos mais SaveChanges nos repositórios
        IProdutoRepository ProdutoRepository {  get; }
        ICategoriaRepository CategoriaRepository { get; }
        void Commit();
    }
}
