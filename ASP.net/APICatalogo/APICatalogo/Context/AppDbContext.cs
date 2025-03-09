using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    // Essa classe faz o mapeamento das entidades e transforma elas em tabela
    // Herdando assim do DbContext (uma classe do EntityFrameworkCore instalado)
    public class AppDbContext : DbContext
    {
        // Criasse um atrbiuto "options" do tipo "DbContextOptions" que herda tudo de DbContext(options), sua "base"
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {        
        }

        // DbSet permite o framework mapear e criar as tabelas(modelos), tendo como nome "Categorias" e parâmetro "Categoria"
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

    }
}

/*
COMANDOS PARA EXECUTAR O MIGRATIONS NO CMD:

Verifica a versão dotnet ---------> dotnet ef 

Adiciona um migration novo -------> dotnet ef migrations add 'nome'

Verifica a lista de migrations ---> dotnet ef migrations list

Remove um migration executado ----> dotnet ef migrations remove 'nome'    (se não inserir "nome" remove o último da lista)

Atualiza o BD após os migrations -> dotnet ef database update
*/
