using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnyMusic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class part5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumPrice",
                table: "Albums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AlbumPrice",
                table: "Albums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
