using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class Add_field_to_consumergroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "numMembers",
                table: "consumerGroup",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numMembers",
                table: "consumerGroup");
        }
    }
}
