using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaneenStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Authers");

            migrationBuilder.AddColumn<string>(
                name: "AutherName",
                table: "Authers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Authers_AutherName",
                table: "Authers",
                column: "AutherName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authers_AutherName",
                table: "Authers");

            migrationBuilder.DropColumn(
                name: "AutherName",
                table: "Authers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Authers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
