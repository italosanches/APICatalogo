using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class InsertTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categorias(CategoriaNome,CategoriaImgUrl) Values('Bebidas','bebidas.jpg')");
			migrationBuilder.Sql("insert into Categorias(CategoriaNome,CategoriaImgUrl) Values('Lanches','lanche.jpg')");
			migrationBuilder.Sql("insert into Categorias(CategoriaNome,CategoriaImgUrl) Values('Sobremesas','sobremesa.jpg')");

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categorias");
        }
    }
}
