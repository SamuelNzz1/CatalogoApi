using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalago.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Produto A', 'Descrição do produto A', 19.99, 'http://exemplo.com/imagemA.jpg', 100, NOW(), 1), ('Produto B', 'Descrição do produto B', 29.99, 'http://exemplo.com/imagemB.jpg', 150, NOW(), 3), ('Produto C', 'Descrição do produto C', 49.99, 'http://exemplo.com/imagemC.jpg', 200, NOW(), 2),('Produto D', 'Descrição do produto D', 9.99, 'http://exemplo.com/imagemD.jpg', 250, NOW(), 5),('Produto E', 'Descrição do produto E', 39.99, 'http://exemplo.com/imagemE.jpg', 300, NOW(), 4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
