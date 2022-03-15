using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDbApi.Migrations
{
    public partial class AndNowThis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "TVSerieses",
                newName: "original_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "original_name",
                table: "TVSerieses",
                newName: "name");
        }
    }
}
