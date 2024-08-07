using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Offset",
                table: "offset",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offset",
                table: "offset");
        }
    }
}
