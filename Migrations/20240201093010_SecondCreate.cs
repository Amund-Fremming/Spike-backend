using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconImageUrl",
                table: "Games",
                newName: "IconImage");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfQuestions",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfQuestions",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "IconImage",
                table: "Games",
                newName: "IconImageUrl");
        }
    }
}
