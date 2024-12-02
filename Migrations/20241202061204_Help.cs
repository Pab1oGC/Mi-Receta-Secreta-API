using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiRecetaSecretaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Help : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Quitar las claves primarias que dependen de la columna actual
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            // Eliminar las columnas actuales con nombre "Id"
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeIngredient");

            // Agregar las nuevas columnas "Id" con la propiedad IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeTag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RecipeIngredient",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Configurar las nuevas claves primarias en base a la nueva columna "Id"
            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                column: "Id");

            // Crear índices si son necesarios
            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_RecipeId",
                table: "RecipeTag",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Quitar las claves primarias que dependen de la columna "Id" recién agregada
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            // Eliminar las columnas "Id" recién agregadas
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipeIngredient");

            // Restaurar las claves primarias compuestas originales
            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeTag",
                table: "RecipeTag",
                columns: new[] { "RecipeId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                columns: new[] { "RecipeId", "IngredientId" });
        }
    }
}
