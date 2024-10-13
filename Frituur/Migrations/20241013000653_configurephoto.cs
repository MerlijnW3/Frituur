using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Frituur.Migrations
{
    /// <inheritdoc />
    public partial class configurephoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoString",
                table: "Product");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Product",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "PhotoString",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
