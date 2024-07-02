using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class MessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "message");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ProducerId",
                table: "message",
                newName: "IX_message_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_PartitionId",
                table: "message",
                newName: "IX_message_PartitionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_message",
                table: "message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_message_partition_PartitionId",
                table: "message",
                column: "PartitionId",
                principalTable: "partition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_message_producer_ProducerId",
                table: "message",
                column: "ProducerId",
                principalTable: "producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_message_partition_PartitionId",
                table: "message");

            migrationBuilder.DropForeignKey(
                name: "FK_message_producer_ProducerId",
                table: "message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_message",
                table: "message");

            migrationBuilder.RenameTable(
                name: "message",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_message_ProducerId",
                table: "Message",
                newName: "IX_Message_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_message_PartitionId",
                table: "Message",
                newName: "IX_Message_PartitionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message",
                column: "PartitionId",
                principalTable: "partition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message",
                column: "ProducerId",
                principalTable: "producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
