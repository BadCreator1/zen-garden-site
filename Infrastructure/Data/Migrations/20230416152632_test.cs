using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Commentaries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Commentaries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Posts_PostId",
                table: "Commentaries",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
