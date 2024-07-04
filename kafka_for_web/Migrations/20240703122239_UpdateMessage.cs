using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_partition_PartitionId",
                table: "message");

            migrationBuilder.RenameColumn(
                name: "PartitionId",
                table: "message",
                newName: "TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_message_PartitionId",
                table: "message",
                newName: "IX_message_TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_message_partition_TopicId",
                table: "message",
                column: "TopicId",
                principalTable: "partition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_partition_TopicId",
                table: "message");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "message",
                newName: "PartitionId");

            migrationBuilder.RenameIndex(
                name: "IX_message_TopicId",
                table: "message",
                newName: "IX_message_PartitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_message_partition_PartitionId",
                table: "message",
                column: "PartitionId",
                principalTable: "partition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
