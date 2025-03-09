using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into apicatalogodb.Categorias(Nome, ImageUrl) Values ('Bebidas','bebidas.jpg')");
            migrationBuilder.Sql("Insert into apicatalogodb.Categorias(Nome, ImageUrl) Values ('Lanches','lanches.jpg')");
            migrationBuilder.Sql("Insert into apicatalogodb.Categorias(Nome, ImageUrl) Values ('Sobremesas','sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}