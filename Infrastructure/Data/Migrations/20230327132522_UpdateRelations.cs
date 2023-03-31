using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockTypeId",
                table: "Blocks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockTypeId",
                table: "Blocks",
                column: "BlockTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks",
                column: "BlockTypeId",
                principalTable: "BlockTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockTypeId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "BlockTypeId",
                table: "Blocks");
        }
    }
}
