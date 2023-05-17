using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJsonDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Posts_PostId",
                table: "Blocks");

            migrationBuilder.AddColumn<string>(
                name: "jsonDoc",
                table: "Posts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Blocks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlockTypeId",
                table: "Blocks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks",
                column: "BlockTypeId",
                principalTable: "BlockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Posts_PostId",
                table: "Blocks",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Posts_PostId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "jsonDoc",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Blocks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BlockTypeId",
                table: "Blocks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_BlockTypes_BlockTypeId",
                table: "Blocks",
                column: "BlockTypeId",
                principalTable: "BlockTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Posts_PostId",
                table: "Blocks",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
