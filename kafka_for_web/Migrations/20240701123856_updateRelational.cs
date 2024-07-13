using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class updateRelational : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition");

            migrationBuilder.AlterColumn<long>(
                name: "TopicId",
                table: "partition",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition",
                column: "TopicId",
                principalTable: "topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition");

            migrationBuilder.AlterColumn<long>(
                name: "TopicId",
                table: "partition",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_partition_topic_TopicId",
                table: "partition",
                column: "TopicId",
                principalTable: "topic",
                principalColumn: "Id");
        }
    }
}
