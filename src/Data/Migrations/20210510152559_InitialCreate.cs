using Microsoft.EntityFrameworkCore.Migrations;

namespace MercadoEletronico.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    idPedido = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pedido = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.idPedido);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    idItem = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idPedido = table.Column<int>(type: "INTEGER", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: true),
                    precoUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    qtd = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.idItem);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_idPedido",
                        column: x => x.idPedido,
                        principalTable: "Pedido",
                        principalColumn: "idPedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_idPedido",
                table: "ItemPedido",
                column: "idPedido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
