using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kafka_for_web.Migrations
{
    /// <inheritdoc />
    public partial class RemovedConsumerGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerGroupId",
                table: "consumer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumerGroupId",
                table: "consumer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_consumer_consumerGroup_ConsumerGroupId",
                table: "consumer",
                column: "ConsumerGroupId",
                principalTable: "consumerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
