using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaDAL.Migrations
{
    /// <inheritdoc />
    public partial class PublisherUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "YearOfPublication",
                table: "Publisher",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "YearOfPublication",
                table: "Publisher",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
