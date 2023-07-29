using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinAPI.Migrations
{
    /// <inheritdoc />
    public partial class newIvField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Iv",
                table: "TableRow",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iv",
                table: "TableRow");
        }
    }
}
