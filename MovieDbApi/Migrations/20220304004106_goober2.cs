using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDbApi.Migrations
{
    public partial class goober2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TV_Serieses",
                table: "TV_Serieses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TV_Networks",
                table: "TV_Networks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Production_Companies",
                table: "Production_Companies");

            migrationBuilder.RenameTable(
                name: "TV_Serieses",
                newName: "TVSerieses");

            migrationBuilder.RenameTable(
                name: "TV_Networks",
                newName: "TVNetworks");

            migrationBuilder.RenameTable(
                name: "Production_Companies",
                newName: "ProductionCompanies");

            migrationBuilder.RenameColumn(
                name: "Original_Title",
                table: "Movies",
                newName: "OriginalTitle");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Collections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Collections",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVSerieses",
                table: "TVSerieses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVNetworks",
                table: "TVNetworks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionCompanies",
                table: "ProductionCompanies",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TVSerieses",
                table: "TVSerieses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TVNetworks",
                table: "TVNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionCompanies",
                table: "ProductionCompanies");

            migrationBuilder.RenameTable(
                name: "TVSerieses",
                newName: "TV_Serieses");

            migrationBuilder.RenameTable(
                name: "TVNetworks",
                newName: "TV_Networks");

            migrationBuilder.RenameTable(
                name: "ProductionCompanies",
                newName: "Production_Companies");

            migrationBuilder.RenameColumn(
                name: "OriginalTitle",
                table: "Movies",
                newName: "Original_Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Collections",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Collections",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TV_Serieses",
                table: "TV_Serieses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TV_Networks",
                table: "TV_Networks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Production_Companies",
                table: "Production_Companies",
                column: "Id");
        }
    }
}
