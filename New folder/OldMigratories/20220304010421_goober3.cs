using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDbApi.Migrations
{
    public partial class goober3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "TVSerieses",
                newName: "popularity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TVSerieses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TVSerieses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TVNetworks",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TVNetworks",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductionCompanies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductionCompanies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Persons",
                newName: "video");

            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "Persons",
                newName: "popularity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Adult",
                table: "Persons",
                newName: "adult");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Video",
                table: "Movies",
                newName: "video");

            migrationBuilder.RenameColumn(
                name: "Popularity",
                table: "Movies",
                newName: "popularity");

            migrationBuilder.RenameColumn(
                name: "Adult",
                table: "Movies",
                newName: "adult");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OriginalTitle",
                table: "Movies",
                newName: "original_title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Keywords",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Keywords",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Collections",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Collections",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "popularity",
                table: "TVSerieses",
                newName: "Popularity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TVSerieses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TVSerieses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "TVNetworks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TVNetworks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ProductionCompanies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductionCompanies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "video",
                table: "Persons",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "popularity",
                table: "Persons",
                newName: "Popularity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Persons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "adult",
                table: "Persons",
                newName: "Adult");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Persons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "video",
                table: "Movies",
                newName: "Video");

            migrationBuilder.RenameColumn(
                name: "popularity",
                table: "Movies",
                newName: "Popularity");

            migrationBuilder.RenameColumn(
                name: "adult",
                table: "Movies",
                newName: "Adult");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "original_title",
                table: "Movies",
                newName: "OriginalTitle");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Keywords",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Keywords",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Collections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Collections",
                newName: "Id");
        }
    }
}
