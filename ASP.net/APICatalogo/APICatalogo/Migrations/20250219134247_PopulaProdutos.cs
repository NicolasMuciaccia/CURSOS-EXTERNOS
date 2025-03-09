using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into apicatalogodb.Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) Values('Coca-Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocacola.jpg', 50, now(),6)");

            migrationBuilder.Sql("Insert into apicatalogodb.Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) Values('Lanche natural', 'Lanche natural de 500g', 8.45, 'lanchenatural.jpg', 10, now(),7)");

            migrationBuilder.Sql("Insert into apicatalogodb.Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) Values('Pudim', 'Pudim em copo com 100g', 6.45, 'pudim.jpg', 20, now(),8)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
