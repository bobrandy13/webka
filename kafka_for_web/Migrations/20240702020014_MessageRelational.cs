using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class MessageRelational : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_producer_Message_MostRecentMessageId",
                table: "producer");

            migrationBuilder.DropIndex(
                name: "IX_producer_MostRecentMessageId",
                table: "producer");

            migrationBuilder.DropColumn(
                name: "MostRecentMessageId",
                table: "producer");

            migrationBuilder.AlterColumn<long>(
                name: "ProducerId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PartitionId",
                table: "Message",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message");

            migrationBuilder.AddColumn<long>(
                name: "MostRecentMessageId",
                table: "producer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProducerId",
                table: "Message",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PartitionId",
                table: "Message",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_producer_MostRecentMessageId",
                table: "producer",
                column: "MostRecentMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_partition_PartitionId",
                table: "Message",
                column: "PartitionId",
                principalTable: "partition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_producer_ProducerId",
                table: "Message",
                column: "ProducerId",
                principalTable: "producer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_producer_Message_MostRecentMessageId",
                table: "producer",
                column: "MostRecentMessageId",
                principalTable: "Message",
                principalColumn: "Id");
        }
    }
}
